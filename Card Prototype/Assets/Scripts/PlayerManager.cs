using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerManager : NetworkBehaviour
{
    //List of network shared objects
    public GameObject AllyDropZone;
    public GameObject AllyHand;
    public GameObject EnemyDropZone;
    public GameObject EnemyHand;

    public override void OnStartClient()
    {
        base.OnStartClient();

        AllyHand = GameObject.Find("AllyHand");
        EnemyHand = GameObject.Find("EnemyHand");
        AllyDropZone = GameObject.Find("AllyDropZone");
        EnemyDropZone = GameObject.Find("EnemyDropZone");
    }
}
