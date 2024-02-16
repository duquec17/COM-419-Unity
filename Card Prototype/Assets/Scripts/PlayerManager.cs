using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerManager : NetworkBehaviour
{

    public GameObject AllyDropZone;
    public GameObject AllyHand;
    public GameObject EnemyDropZone;
    public GameObject EnemyHand;

    public override void OnStartClient()
    {
        base.OnStartClient();

        AllyDropZone = GameObject.Find("AllyDropZone");
    }
}
