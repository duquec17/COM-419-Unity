using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerManager : NetworkBehaviour
{
    //List of network shared objects
    public GameObject PlayerCard;

    public GameObject AllyDropZone;
    public GameObject AllyDropZone1;
    public GameObject AllyDropZone2;
    public GameObject AllyDropZone3;
    public GameObject AllyDropZone4;
    public GameObject AllyHand;

    public GameObject EnemyDropZone;
    public GameObject EnemyHand;

    public static List<Card> cardList = new List<Card>();

    //Starts client
    public override void OnStartClient()
    {
        base.OnStartClient();

        AllyHand = GameObject.Find("AllyHand");
        EnemyHand = GameObject.Find("EnemyHand");

        AllyDropZone = GameObject.Find("AllyDropZone");
        AllyDropZone1 = GameObject.Find("AllyDropZone (1)");
        AllyDropZone2 = GameObject.Find("AllyDropZone (2)");
        AllyDropZone3 = GameObject.Find("AllyDropZone (3)");
        AllyDropZone4 = GameObject.Find("AllyDropZone (4)");

        EnemyDropZone = GameObject.Find("EnemyDropZone");

        Debug.Log("OnStartClient was activated");
    }

    //Starts server (Host + Client)
    [Server]
    public override void OnStartServer()
    {
        base.OnStartServer();

        cardList.Add(new Card(0, "Jester", 1, 1, 2, "+1 power for every other Jester card on the board", Resources.Load<Sprite>("placeHolder")));
        Debug.Log(cardList);
        Debug.Log("OnStartServer was activated");
    }

    //Command that adds cards to players hands and grants authority
    [Command]
    public void CmdDealCards()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject card = Instantiate(PlayerCard, new Vector2(0, 0), Quaternion.identity);
            NetworkServer.Spawn(card, connectionToClient);
            Debug.Log("Drawing card" + i);
            RpcShowCard(card, "Dealt");
        }
    }

    //Calls upon CmdPlayCard
    public void PlayCard(GameObject card)
    {
        CmdPlayCard(card);
    }

    [Command]
    void CmdPlayCard(GameObject card)
    {
        RpcShowCard(card, "Played");
    }

    [ClientRpc]
    void RpcShowCard(GameObject card, string type)
    {
        if(type == "Dealt")
        {
            if (isOwned)
            {
                card.transform.SetParent(AllyHand.transform, false);
            }
            else
            {
                card.transform.SetParent(EnemyHand.transform, false);
            }
        }
        else if(type == "Played")
        {
            if (isOwned)
            {
                card.transform.SetParent(AllyDropZone.transform, false);
            }
            else
            {
                card.transform.SetParent(EnemyDropZone.transform, false);
            }
        }
    }
}
