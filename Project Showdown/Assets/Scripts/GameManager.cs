using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Spawn")]
    public List<Transform> spawnPoints;

    [Header("Players Management")]
    public int playerCount;
    public List<PlayerData> playerList;
    public List<PlayerHeight> playerHeights;

    [Header("Characters")]
    public CharacterSO[] characters;
    
    [Header("Colors")]
    public List<Color> playersColor;
    public List<Color> playersPossibleColor;

    [Header("Camera")]
    public CinemachineTargetGroup camGroupList;

    [Header("Pause")]
    public static bool gameIsPaused;

    void Awake()
    {
        gameIsPaused = false;

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
