using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameCamera : MonoBehaviour
{
    CinemachineVirtualCamera cam;

    [SerializeField] float difference;
    [SerializeField] float maxDifference;

    [SerializeField] float baseCamOffset;
    [SerializeField] float maxCamOffset;

    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        if(GameManager.instance.playerHeights.Count > 0)
        {
            GameManager.instance.playerHeights.Sort();
            difference = GameManager.instance.playerHeights[0].gameObject.transform.position.y - GameManager.instance.playerHeights[GameManager.instance.playerHeights.Count-1].gameObject.transform.position.y;
            //print(GameManager.instance.playerHeights.Count-1);
            cam.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY = Mathf.Lerp(baseCamOffset, maxCamOffset, difference/maxDifference);

        }
    }
}
