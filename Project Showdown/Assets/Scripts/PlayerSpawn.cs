using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    Transform selfTransform;


    void Start()
    {
        selfTransform = GetComponent<Transform>();
        selfTransform.position = GameManager.instance.spawnPoints[Random.Range(0, GameManager.instance.spawnPoints.Length)].position;
    }
}
