using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   // References to game objects
    public GameObject PlayerCard;
    public GameObject Hand;
    public GameObject Deck;
    //Variables used for networking and determining game logic
    public PlayerManager PlayerManager;
    public GameManager GameManager;
    public List<GameObject> DropZones = new List<GameObject>();

    // Variables for game state
    public int Health;
    public int CardsPlayed = 0;
    public bool IsMyTurn = false;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize references to game objects
        Hand = GameObject.Find("AllyHand");
        Deck = GameObject.Find("Deck");



        // Connect drop zones to the list
        DropZones.Add(GameObject.Find("AllyDropZone"));
        DropZones.Add(GameObject.Find("AllyDropZone (1)"));
        DropZones.Add(GameObject.Find("AllyDropZone (2)"));
        DropZones.Add(GameObject.Find("AllyDropZone (3)"));
        DropZones.Add(GameObject.Find("AllyDropZone (4)"));
        DropZones.Add(GameObject.Find("AllyDropZone (5)"));

        // Draw initial hand of cards
        for (int i = 0; i < 3; i++)
        {
            PlayerManager.CmdDealCards();
        }
    }

    // Method to draw a card into the player's hand
    public void DrawCard()
    {
        GameObject card = Instantiate(PlayerCard, Deck.transform.position, Quaternion.identity);
        card.transform.SetParent(Hand.transform, false);
    }

    // Method to play a card from the player's hand
    public void PlayCard(GameObject card)
    {
        // Implement logic to play the card
        // For example, move it from the hand to the game board
        card.transform.SetParent(DropZones[CardsPlayed].transform, false);
        CardsPlayed++;
    }

    // Method to end the player's turn
    public void EndTurn()
    {
        // Implement logic to end the turn
        IsMyTurn = false;
        // Notify the GameManager or other relevant scripts
    }
}
