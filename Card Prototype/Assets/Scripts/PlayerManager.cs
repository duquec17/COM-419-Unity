using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerManager : NetworkBehaviour
{
    //Start of all network shared objects & variables//
    public GameManager GameManager;
    public GameObject PlayerCard;
    public DropZonePosition dropZonePosition;

    //All ally objects
    public GameObject AllyDropZone;
    public GameObject AllyDropZone1;
    public GameObject AllyDropZone2;
    public GameObject AllyDropZone3;
    public GameObject AllyDropZone4;
    public GameObject AllyHand;
    public GameObject AllyDeck;
    
    //All enemy object
    public GameObject EnemyDropZone;
    public GameObject EnemyDropZone1;
    public GameObject EnemyDropZone2;
    public GameObject EnemyDropZone3;
    public GameObject EnemyDropZone4;
    public GameObject EnemyHand;
    public GameObject EnemyDeck;

    //Lists that will hold ally and enemy drop zones resepctively
    public List<GameObject> AllyDropZones = new List<GameObject>();
    public List<GameObject> EnemyDropZones = new List<GameObject>();

    //Possible alternative Card List; same purpose but better way of getting it
    private List<GameObject> cards = new List<GameObject>();

    //Card list we are pulling from
    public static List<Card> cardList = new List<Card>();

    //Variable tracking current player
    public bool IsMyTurn = false;

    //Variables that are constantly kept synced across the network; Think seeing health all the time
    //Need to be used later combat system 
    [SyncVar]
    int CardsPlayed = 0;
    int Health = 20;

    //End of declaration//


    //Start of actual code//

    //Runs the code below when Host+Client/Client button is selected
    public override void OnStartClient()
    {
        base.OnStartClient();

        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //Connects hand and deck of both players to a variable by looking for objects with listed name 
        AllyHand = GameObject.Find("AllyHand");
        AllyDeck = GameObject.Find("AllyDeck");
        EnemyHand = GameObject.Find("EnemyHand");
        EnemyDeck = GameObject.Find("EnemyDeck");

        //Connects each ally drop zones to each variable by looking for objects with listed name 
        AllyDropZone = GameObject.Find("AllyDropZone");
        AllyDropZone1 = GameObject.Find("AllyDropZone (1)");
        AllyDropZone2 = GameObject.Find("AllyDropZone (2)");
        AllyDropZone3 = GameObject.Find("AllyDropZone (3)");
        AllyDropZone4 = GameObject.Find("AllyDropZone (4)");

        //Connects each enemy drop zones to each variable by looking for objects with listed name 
        EnemyDropZone = GameObject.Find("EnemyDropZone");
        EnemyDropZone1 = GameObject.Find("EnemyDropZone (1)");
        EnemyDropZone2 = GameObject.Find("EnemyDropZone (2)");
        EnemyDropZone3 = GameObject.Find("EnemyDropZone (3)");
        EnemyDropZone4 = GameObject.Find("EnemyDropZone (4)");

        //Fills Ally list with ally drop zones
        AllyDropZones.Add(AllyDropZone);
        AllyDropZones.Add(AllyDropZone1);
        AllyDropZones.Add(AllyDropZone2);
        AllyDropZones.Add(AllyDropZone3);
        AllyDropZones.Add(AllyDropZone4);

        //Fills Enemy list will enemy drop zones
        EnemyDropZones.Add(EnemyDropZone);
        EnemyDropZones.Add(EnemyDropZone1);
        EnemyDropZones.Add(EnemyDropZone2);
        EnemyDropZones.Add(EnemyDropZone3);
        EnemyDropZones.Add(EnemyDropZone4);

        //Determines which player goes first; Whoever presses "Client" button goes first
        if (isClientOnly)
        {
            IsMyTurn = true;
        }

        //Message to show that client is running; appears in console tab
        Debug.Log("OnStartClient was activated");
    }

    //Starts server (Host + Client)
    [Server]
    public override void OnStartServer()
    {
        base.OnStartServer();

        //Adds a card to current card list
        cardList.Add(new Card(0, "Jester", 1, 1, 2, "+1 power for every other Jester card on the board", Resources.Load<Sprite>("placeHolder")));
        
        //Should output current card list to console
        Debug.Log(cardList);

        //Tells us that the server is running
        Debug.Log("OnStartServer was activated");
    }

    //Command that adds cards to players hands and grants authority
    [Command]
    public void CmdDealCards()
    {
        //Fills player's hand with a card and repeats 3 times
        for (int i = 0; i < 3; i++)
        {
            GameObject card = Instantiate(PlayerCard, new Vector2(0, 0), Quaternion.identity);
            NetworkServer.Spawn(card, connectionToClient);

            //Tells which card has been drawn
            Debug.Log("Drawing card" + i);

            //Sets current status of the card to "Dealt" which makes it appear in hand for the Server
            RpcShowCard(card, "Dealt");
        }

        //Changes game manager current state
        RpcGMChangeState("Compile");
    }

    //Calls upon CmdPlayCard
    public void PlayCard(GameObject card)
    {
        CmdPlayCard(card);
    }

    //Calls upon RocShowCard
    [Command]
    void CmdPlayCard(GameObject card)
    {
        RpcShowCard(card, "Played"); //Sets current status of the card to "Played" which makes it appear in a drop zone for the Server
    }

    //Actual function that moves the recently added card to hand and/or drop zone
    [ClientRpc]
    void RpcShowCard(GameObject card, string type)
    {
        if(type == "Dealt") // "Dealt" cards are placed into player's hand and checking ownership allows for it mirror
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
        else if(type == "Played") // "Played" cards are placed into a player's drop zone and check mirror condition
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

    [ClientRpc]
    void RpcGMChangeState(string stateRequest)
    {
        GameManager.ChangeGameState(stateRequest);
    }
}
