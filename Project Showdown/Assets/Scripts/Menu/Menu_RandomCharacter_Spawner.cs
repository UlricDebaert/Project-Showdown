using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_RandomCharacter_Spawner : MonoBehaviour
{

    public GameObject characterPrefab;
    public CharacterSO[] charactersList;

    public Transform spawnPoint;

    public Vector2 randomSpawnTime;
    float spawnTimer;

    void Start()
    {
        spawnTimer = Random.Range(randomSpawnTime.x, randomSpawnTime.y);
    }


    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if(spawnTimer <= 0)
        {
            SpawnCharacter();
            spawnTimer = Random.Range(randomSpawnTime.x, randomSpawnTime.y);
        }
    }

    void SpawnCharacter()
    {
        int rand = Random.Range(0, charactersList.Length);

        GameObject characterSpawned = Instantiate(characterPrefab, spawnPoint.position, Quaternion.identity);
        characterSpawned.GetComponent<Menu_RandomCharacter>().id = rand;

        GameObject gun = new GameObject("gun", typeof(SpriteRenderer));
        gun.transform.parent = characterSpawned.transform;
        gun.transform.localPosition = charactersList[rand].gunPos;
        gun.GetComponent<SpriteRenderer>().sprite = charactersList[rand].gun.GetComponent<Gun>().gunStats.gunSprite;
        gun.GetComponent<SpriteRenderer>().sortingOrder = 11;
    }
}
