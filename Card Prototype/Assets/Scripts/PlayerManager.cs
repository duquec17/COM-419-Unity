using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerManager : NetworkBehaviour
{
    //List of network shared objects
    public GameObject PlayerCard;
    public GameObject AllyDropZone;
    public GameObject AllyHand;
    public GameObject EnemyDropZone;
    public GameObject EnemyHand;

    public static List<Card> cardList = new List<Card>();

    public override void OnStartClient()
    {
        base.OnStartClient();

        AllyHand = GameObject.Find("AllyHand");
        EnemyHand = GameObject.Find("EnemyHand");
        AllyDropZone = GameObject.Find("AllyDropZone");
        EnemyDropZone = GameObject.Find("EnemyDropZone");

        Debug.Log("OnStartClient was activated");
    }

    [Server]
    public override void OnStartServer()
    {
        base.OnStartServer();

        cardList.Add(new Card(0, "Jester", 1, 1, 2, "+1 power for every other Jester card on the board", Resources.Load<Sprite>("placeHolder")));
        Debug.Log(cardList);
        Debug.Log("OnStartServer was activated");
    }


    [Command]
    public void CmdDealCards()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject card = Instantiate(PlayerCard, new Vector2(0, 0), Quaternion.identity);
            card.transform.SetParent(AllyHand.transform, false);
            Debug.Log("Drawing card" + i);
        }
    }
}
