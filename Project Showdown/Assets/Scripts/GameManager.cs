using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Spawn")]
    public Transform[] spawnPoints;

    [Header("Players Management")]
    public int playerCount;

    [Header("Characters")]
    public CharacterSO[] characters;

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
}
