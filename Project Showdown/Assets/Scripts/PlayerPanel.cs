using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPanel : MonoBehaviour
{
    Transform ownTransform;
    UIManager manager;


    void Start()
    {
        ownTransform = GetComponent<Transform>();
        manager = UIManager.instance;
        ownTransform.parent = manager.playerScrollView.transform;
    }
}
