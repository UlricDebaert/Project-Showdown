using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
using UnityEngine.InputSystem;

//Custom Inspector
using UnityEditor;


[CreateAssetMenu]
public class Gun : MonoBehaviour
{
    [Tooltip("Gun stats reference")] public GunSO gunStats;
    [HideInInspector] public Transform firePoint;

    SpriteRenderer gunSprite;
    AudioSource audioSource;
    Animator anim;
    AnimatorOverrideController animOverrideController;

    int currentAmmoCount;
    float reloadTimer;

    bool canShoot;
    float fireRateTimer;

    [Tooltip("Particle system for bullet shell ejection")] [HideInInspector] public GameObject bulletShellPrefab;
    [HideInInspector] public ParticleSystem bulletShellEffect;

    //public RuntimeAnimatorController animatorController;

    //specific to fire mode
    bool barrelEmpty;

    Controls inputActions;
    Vector2 lookPosition;
    Camera mainCamera;

    [Header("Debug")]
    public AnimatorController originalAnimationController;

    private void Awake()
    {
        inputActions = GetComponentInParent<Controls>();
        inputActions.Shoot.Aim.performed += ctx => lookPosition = ctx.ReadValue<Vector2>();
    }

    void Start()
    {
        gunSprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        InitAnimOverrideController();

        anim.Play("Gun_Idle_Anim");

        canShoot = false;
        barrelEmpty = false;

        currentAmmoCount = gunStats.magazine;
        UpdateAmmoCount();
        if (AmmoCountPannel.instance != null && AmmoCountPannel.instance.ammoLoadingIcon != null)
            AmmoCountPannel.instance.ammoLoadingIcon.fillAmount = 0.0f;
    }


    void Update()
    {
        //LookToMousePosition();
        UpdateFireRate();
        UpdateGraphics();
        CheckInput();
        UpdateReloading();
        Aim();
    }

    void CheckInput()
    {
        switch (gunStats.fireMode)
        {
            case GunSO.shootType.fullAuto:
                if (inputActions.Shoot.Fire.triggered && canShoot && currentAmmoCount > 0)
                {
                    Fire();
                    if(bulletShellEffect != null) bulletShellEffect.Play();
                }
                break;

            case GunSO.shootType.semiAuto:
                if (inputActions.Shoot.Fire.triggered && canShoot && currentAmmoCount > 0)
                {
                    Fire();
                    if (bulletShellEffect != null) bulletShellEffect.Play();
                }
                break;

            case GunSO.shootType.pump:
                if (inputActions.Shoot.Fire.triggered && barrelEmpty)
                {
                    Invoke("EmptyBarrel", 0.1f);

                    if(gunStats.cockingAnimation != null)
                        anim.Play("Gun_Cocking_Anim");

                    if (bulletShellEffect != null) bulletShellEffect.Play();
                }
                if (inputActions.Shoot.Fire.triggered && canShoot && currentAmmoCount > 0 && !barrelEmpty)
                {
                    Fire();
                    Invoke("EmptyBarrel", 0.1f);
                }
                break;

        }
    }

    void Fire()
    {
        if (gunStats.shootAnimation != null)
            anim.Play("Gun_Shoot_Anim");

        for (int j = 1; j <= gunStats.bulletQuantityPerShootPoint; j++)
        {
            GameObject bullet = Instantiate(gunStats.bulletPrefab, firePoint.position, firePoint.transform.rotation * Quaternion.Euler(0.0f, 0.0f, Random.Range(-gunStats.bulletAngleShift, gunStats.bulletAngleShift)));
            bullet.GetComponent<Rigidbody2D>().AddForce(gunStats.bulletSpeed * bullet.transform.right.normalized, ForceMode2D.Impulse);
            bullet.GetComponent<Bullet>().bulletDamage = gunStats.bulletDamage;
            bullet.GetComponent<Bullet>().maxTargetsPenetration = gunStats.maxTargetsPenetration;
            bullet.GetComponent<Bullet>().penetrationMultiplier = gunStats.penetrationMultiplier;
            //bullet.GetComponent<Bullet>().knockbackOnTarget = gunStats.knockbackOnTarget;
        }


        if (gunStats.muzzleflashPrefabs.Length != 0)
        {
            int rand = Random.Range(0, gunStats.muzzleflashPrefabs.Length - 1);
            GameObject flash = Instantiate(gunStats.muzzleflashPrefabs[rand], firePoint.position, firePoint.transform.rotation, gameObject.transform);
            Destroy(flash, gunStats.muzzleflashLifeTime);
        }

        canShoot = false;
        fireRateTimer = gunStats.fireRate;
        currentAmmoCount--;

        if (currentAmmoCount <= 0)
        {
            reloadTimer = gunStats.reloadTime;
        }
                
        gameObject.GetComponentInParent<Rigidbody2D>().AddForce(
            new Vector2(Mathf.Sign(-(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x)), 0) * gunStats.knockbackOnPlayer, 
            ForceMode2D.Impulse);

        if (gunStats.shootAudio != null)
        {
            audioSource.clip = gunStats.shootAudio;
            audioSource.volume = gunStats.volumeBase;
            audioSource.PlayOneShot(audioSource.clip);
            audioSource.pitch=gunStats.pitchBase+Random.Range(-gunStats.pitchVariation, gunStats.pitchVariation);
        }


        UpdateAmmoCount();
    }

    void UpdateFireRate()
    {
        if (fireRateTimer>0.0f)
        {
            fireRateTimer -= Time.deltaTime;

        }
        else { canShoot = true; }
    }

    void UpdateReloading()
    {
        if (reloadTimer > 0.0f)
        {
            anim.SetBool("emptyMag", true);

            reloadTimer -= Time.deltaTime;

            if (AmmoCountPannel.instance != null && AmmoCountPannel.instance.ammoLoadingIcon != null && reloadTimer > 0.0f) 
                AmmoCountPannel.instance.ammoLoadingIcon.fillAmount = 1-reloadTimer/gunStats.reloadTime;

            if (reloadTimer <= 0.0f)
            {
                currentAmmoCount = gunStats.magazine;

                if (AmmoCountPannel.instance != null && AmmoCountPannel.instance.ammoLoadingIcon != null) 
                    AmmoCountPannel.instance.ammoLoadingIcon.fillAmount = 0.0f;

                anim.SetBool("emptyMag", false);

                UpdateAmmoCount();
            }
        }
    }

    void LookToMousePosition()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);
    }

    void Aim()
    {
        transform.localEulerAngles = new Vector3(0f, 0f, Mathf.Atan2(lookPosition.x, lookPosition.y)*-180/Mathf.PI+90f);
    }

    void UpdateGraphics()
    {
        if(transform.eulerAngles.z >= 90 && transform.eulerAngles.z <= 260)
        {
            gunSprite.flipY = true;
            if(bulletShellEffect!=null)
                bulletShellEffect.gameObject.transform.localEulerAngles = new Vector3(90, bulletShellEffect.gameObject.transform.eulerAngles.y, bulletShellEffect.gameObject.transform.eulerAngles.z);
        }
        else
        {
            gunSprite.flipY = false;
            if (bulletShellEffect != null)
                bulletShellEffect.gameObject.transform.localEulerAngles = new Vector3(-90, bulletShellEffect.gameObject.transform.eulerAngles.y, bulletShellEffect.gameObject.transform.eulerAngles.z);
        }
    }

    void UpdateAmmoCount()
    {
        if (AmmoCountPannel.instance != null && AmmoCountPannel.instance.ammoCounterText != null)
        {
            AmmoCountPannel.instance.ammoCounterText.text = currentAmmoCount.ToString() + " / " + gunStats.magazine.ToString();
        }
    }

    void EmptyBarrel()
    {
        barrelEmpty = !barrelEmpty;
    }

    void InitAnimOverrideController()
    {
        animOverrideController = new AnimatorOverrideController(AssetDatabase.LoadAssetAtPath<AnimatorController>("Assets/DontTouch/Animator/Guns_Animator.controller")); //Must Write the exact Controller path
        //print("Path to reference : " + AssetDatabase.GetAssetPath(originalAnimationController)); //Print Controller Asset Path

        if (originalAnimationController != null) 
            animOverrideController = new AnimatorOverrideController(AssetDatabase.LoadAssetAtPath<AnimatorController>(AssetDatabase.GetAssetPath(originalAnimationController)));

        if (anim.runtimeAnimatorController != null)
            anim.runtimeAnimatorController = animOverrideController;

        animOverrideController["Gun_Shoot_Anim"] = gunStats.shootAnimation;
        animOverrideController["Gun_Idle_Anim"] = gunStats.idleAnimation;
        animOverrideController["Gun_EmptyMag_Anim"] = gunStats.emptyMagazineAnimation;
        if(gunStats.fireMode == GunSO.shootType.pump)
            animOverrideController["Gun_Cocking_Anim"] = gunStats.cockingAnimation;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}



[CustomEditor(typeof(Gun))]
public class Gun_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); 

        Gun script = (Gun)target;

        if (!script.gameObject.GetComponent<SpriteRenderer>())
        {
            SpriteRenderer sprite = script.gameObject.AddComponent<SpriteRenderer>();
            sprite.sortingOrder = 30;
        }

        if(script.gunStats != null)
        {
            if (script.gunStats.gunSprite != null)
            {
                if (script.gameObject.GetComponent<SpriteRenderer>().sprite == null || script.gameObject.GetComponent<SpriteRenderer>().sprite != script.gunStats.gunSprite)
                {
                    Debug.Log("Sprite Change");
                    script.gameObject.GetComponent<SpriteRenderer>().sprite = script.gunStats.gunSprite;
                }
            }
        }

        if (!script.gameObject.GetComponent<Animator>())
        {
            script.gameObject.AddComponent<Animator>();
        }

        //if(script.gameObject.GetComponent<Animator>() 
        //    && (script.gameObject.GetComponent<Animator>().runtimeAnimatorController == null || script.animatorController) 
        //    && script.animatorController != null)
        //{
        //    script.gameObject.GetComponent<Animator>().runtimeAnimatorController = script.animatorController;
        //}

        //if (script.gameObject.GetComponent<Animator>())
        //{
        //    script.gameObject.GetComponent<Animator>().runtimeAnimatorController = AssetDatabase.LoadAssetAtPath<AnimatorOverrideController>("Guns_Animator_Override");
        //}

        if (!script.gameObject.GetComponent<AudioSource>())
        {
            AudioSource audio = script.gameObject.AddComponent<AudioSource>();
            audio.playOnAwake = false;
        }

        if(script.firePoint == null)
        {
            GameObject shootPoint = new GameObject();
            shootPoint.transform.parent = script.transform;
            shootPoint.name = "Shoot Point";
            script.firePoint = shootPoint.transform;
        }

        if (script.gunStats.bulletShellEffect != null) script.bulletShellPrefab = script.gunStats.bulletShellEffect;

        if (script.bulletShellEffect != null && script.bulletShellPrefab != null)
        {
            if (script.bulletShellPrefab.name != script.bulletShellEffect.name)
            {
                DestroyImmediate(script.bulletShellEffect.gameObject);
                GameObject bulletShell = Instantiate(script.bulletShellPrefab, script.transform);
                script.bulletShellEffect = bulletShell.GetComponent<ParticleSystem>();
                bulletShell.name = script.bulletShellPrefab.name;
            }
        }

        if(script.bulletShellEffect == null && script.bulletShellPrefab != null)
        {
            Debug.Log("Instantiate");
            GameObject bulletShell = Instantiate(script.bulletShellPrefab, script.transform);
            script.bulletShellEffect = bulletShell.GetComponent<ParticleSystem>();
            bulletShell.name = script.bulletShellPrefab.name;
        }
    }
}