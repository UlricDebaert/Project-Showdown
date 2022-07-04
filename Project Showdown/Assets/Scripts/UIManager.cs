using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject playerScrollView;

    public GameObject playerPanelPrefab;
    public List<PlayerPanel> playerPanels = new List<PlayerPanel>();

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

        playerPanels = new List<PlayerPanel>();
    }

    public void AddPlayerPanel()
    {
        GameObject pannel = Instantiate(playerPanelPrefab);
        playerPanels.Add(pannel.GetComponent<PlayerPanel>());
    }
}
