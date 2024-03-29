using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerManager : NetworkBehaviour
{
    //Start of all network shared objects & variables//
    public GameManager GameManager;
    public GameObject PlayerCard;
    public CombatManager CombatManager;


    //All ally objects
    public int AllyHealth;
    public GameObject AllyDropZone1;
    public GameObject AllyDropZone2;
    public GameObject AllyDropZone3;
    public GameObject AllyDropZone4;
    public GameObject AllyDropZone5;
    public GameObject AllyHand;
    public GameObject AllyDeck;
    
    //All enemy object
    public int EnemyHealth;
    public GameObject EnemyDropZone1;
    public GameObject EnemyDropZone2;
    public GameObject EnemyDropZone3;
    public GameObject EnemyDropZone4;
    public GameObject EnemyDropZone5;
    public GameObject EnemyHand;
    public GameObject EnemyDeck;

    //Lists that will hold ally and enemy drop zones resepctively
    public List<GameObject> AllyDropZones = new List<GameObject>();
    public List<GameObject> EnemyDropZones = new List<GameObject>();

    //Card list we are pulling from
    public static List<Card> cardList = new List<Card>();

    //Variable tracking current player
    public int CardsPlayed = 0;
    [SyncVar]
    public int TurnsEnded = 0;


    public bool IsMyTurn = false;

    //Possible alternative Card List; same purpose but better way of getting it
    private List<GameObject> cards = new List<GameObject>();

    //Runs the code below when Host+Client/Client button is selected
    public override void OnStartClient()
    {
        base.OnStartClient();

        //Connect to GameManager
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //Connects hand and deck of both players to a variable by looking for objects with listed name 
        AllyHand = GameObject.Find("AllyHand");
        AllyDeck = GameObject.Find("AllyDeck");
        EnemyHand = GameObject.Find("EnemyHand");
        EnemyDeck = GameObject.Find("EnemyDeck");

        //Connects each ally drop zones to each variable by looking for objects with listed name 
        AllyDropZone1 = GameObject.Find("AllyDropZone");
        AllyDropZone2 = GameObject.Find("AllyDropZone (1)");
        AllyDropZone3 = GameObject.Find("AllyDropZone (2)");
        AllyDropZone4 = GameObject.Find("AllyDropZone (3)");
        AllyDropZone5 = GameObject.Find("AllyDropZone (4)");

        //Connects each enemy drop zones to each variable by looking for objects with listed name 
        EnemyDropZone1 = GameObject.Find("EnemyDropZone");
        EnemyDropZone2 = GameObject.Find("EnemyDropZone (1)");
        EnemyDropZone3 = GameObject.Find("EnemyDropZone (2)");
        EnemyDropZone4 = GameObject.Find("EnemyDropZone (3)");
        EnemyDropZone5 = GameObject.Find("EnemyDropZone (4)");

        //Fills Ally list with ally drop zones
        AllyDropZones.Add(AllyDropZone1);
        AllyDropZones.Add(AllyDropZone2);
        AllyDropZones.Add(AllyDropZone3);
        AllyDropZones.Add(AllyDropZone4);
        AllyDropZones.Add(AllyDropZone5);

        //Fills Enemy list will enemy drop zones
        EnemyDropZones.Add(EnemyDropZone1);
        EnemyDropZones.Add(EnemyDropZone2);
        EnemyDropZones.Add(EnemyDropZone3);
        EnemyDropZones.Add(EnemyDropZone4);
        EnemyDropZones.Add(EnemyDropZone5);


        //Determines which player goes first; Whoever presses "Client" button goes first
        if (isClientOnly)
        {
            IsMyTurn = true;
        }
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

            //Sets current status of the card to "Dealt" which makes it appear in hand for the Server
            RpcShowCard(card, "Dealt");

        }

        //Changes game manager current state
        RpcGMChangeState("Compile {}");
    }

    //Calls upon CmdPlayCard
    public void PlayCard(GameObject card)
    {
        CmdPlayCard(card);
        Debug.Log("Cards Played counter: " + CardsPlayed);
    }

    //Calls upon RocShowCard
    [Command]
    void CmdPlayCard(GameObject card)
    {
        RpcShowCard(card, "Played"); //Sets current status of the card to "Played" which makes it appear in a drop zone for the Server
    }

    //Command to call function that checks if player ends their turn
    [Command]
    public void CmdEndTurn()
    {
        Debug.Log("TurnsEnded = " + TurnsEnded);
        RpcEndTurn();

        if (TurnsEnded == 1)
        {
            //Changes game manager current state
            RpcGMChangeState("Execute {}");
        }
        Debug.Log("TurnsEnded = " + TurnsEnded);
    }

    [ClientRpc]
    public void RpcEndTurn()
    {
        PlayerManager pm = NetworkClient.connection.identity.GetComponent<PlayerManager>();
        pm.IsMyTurn = !pm.IsMyTurn;
        Debug.Log("Ran RpcEndTurn");
    }

    //Command that initializes combat system
    [Command]
    public void CmdCombatSystem()
    {
        CombatManager.CompareCards();
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
            if (CardsPlayed == 5)
            {
                CardsPlayed = 5;
            }

            if (isOwned)
            {
                //card.transform.SetParent(AllyDropZones[CardsPlayed].transform, false);
                card.transform.SetParent(AllyDropZones[CardsPlayed].transform, false);
            }
            else if (!isOwned)
            {
                card.transform.SetParent(EnemyDropZones[CardsPlayed].transform, false);
            }

            //Increases card counter
            CardsPlayed++;
            Debug.Log("Card ID: " + card.name);
        }
    }

    //Changes GameState by passing Game State name and calling function
    //Check Gamemanger to see function def. (TLDR: ReadyClicks var +1)
    [ClientRpc]
    void RpcGMChangeState(string stateRequest)
    {
        GameManager.ChangeGameState(stateRequest);
        if (stateRequest == "Compile {}")
        {
            GameManager.ChangeReadyClicks();
        }

        if(stateRequest == "Execute {}")
        {
            GameManager.ChangeReadyClicks();
        }
    }

}