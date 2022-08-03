using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Spawn")]
    public Transform[] spawnPoints;

    [Header("Players Management")]
    public int playerCount;

    [Header("Characters")]
    public CharacterSO[] characters;

    [Header("Camera")]
    public CinemachineTargetGroup camGroupList;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void AddPlayer()
    {
        playerCount += 1;
        UIManager.instance.AddPlayerPanel();
    }

    public void AddToCamGroup(GameObject player)
    {
        camGroupList.AddMember(player.transform,1,2);
    }
}
