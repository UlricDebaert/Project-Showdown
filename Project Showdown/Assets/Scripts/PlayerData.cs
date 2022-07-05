using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int playerID;

    public int killCount;
    public int deathCount;

    public int healthPoint = 100;

    private void Start()
    {
        killCount = 0;
        deathCount = 0;

        playerID = GameManager.instance.playerCount;
        GameManager.instance.AddPlayer();

        UIManager.instance.playerPanels[playerID].UpdateKDText(killCount, deathCount);
    }

    public void TakeDamage(int damage)
    {
        healthPoint -= damage;
    }

    public void Death()
    {
        IncreaseDeathCount();
        Destroy(gameObject);
    }

    public void IncreaseKillCount()
    {
        print("KC");
        killCount++;
        UIManager.instance.playerPanels[playerID].UpdateKDText(killCount, deathCount);
    }

    public void IncreaseDeathCount()
    {
        deathCount++;
        UIManager.instance.playerPanels[playerID].UpdateKDText(killCount, deathCount);
    }
}
