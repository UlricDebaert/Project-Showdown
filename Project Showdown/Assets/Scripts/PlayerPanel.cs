using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerPanel : MonoBehaviour
{
    Transform ownTransform;
    UIManager manager;

    public TMP_Text characterName;
    public Image characterIcon;
    public Image playerColor;

    public TMP_Text KDText;
    public TMP_Text AmmoText;

    public Image loadingIcon;

    public Image SPIcon;
    public Image SPLoadingIcon;

    void Start()
    {
        ownTransform = GetComponent<Transform>();
        manager = UIManager.instance;
        ownTransform.parent = manager.playerScrollView.transform;
    }

    public void UpdateKDText(int kill, int death)
    {
        KDText.text = kill.ToString() + " / " + death.ToString();
    }

    public void UpdateAmmoCount(int currentAmmo, int maxAmmo)
    {
        AmmoText.text = "Ammo: " + currentAmmo.ToString() + " / " + maxAmmo.ToString();
    }
}
