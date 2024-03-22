using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{

    public static List<Card> cardList = new List<Card>();

    void Awake()
    {

        cardList.Add(new Card(0, "Jester", 1, 1, 2, "+1 power for every other Jester card on the board", Resources.Load<Sprite>("Jester")));
        cardList.Add(new Card(1, "Jester", 1, 1, 2, "+1 power for every other Jester card on the board", Resources.Load<Sprite>("Jester")));
        cardList.Add(new Card(2, "Jester", 1, 1, 2, "+1 power for every other Jester card on the board", Resources.Load<Sprite>("Jester")));
        cardList.Add(new Card(3, "One Man Circus", 4, 0, 4, "Players health cannot be damaged until this card is destroyed", Resources.Load<Sprite>("OneManCircus")));
        cardList.Add(new Card(4, "Knife Thrower", 3, 1, 2, "Attacks three seperate lanes", Resources.Load<Sprite>("KnifeThrower")));
        cardList.Add(new Card(5, "Psycho Clown", 3, 4, 1, "+1 power every time an ally card is destroyed", Resources.Load<Sprite>("PsychoClown")));
        cardList.Add(new Card(6, "Volunteer", 1, 1, 1, "Moves to an empty lane to protect from incoming damage", Resources.Load<Sprite>("Volunteer")));
        cardList.Add(new Card(7, "Lion Tamer", 8, 2, 2, "Spawns 2 4|4 Tiger cards in the players hand", Resources.Load<Sprite>("LionTamer")));
        cardList.Add(new Card(8, "Ring Leader", 10, 4, 4, "+2 health to all allies on the board", Resources.Load<Sprite>("Ringmaster")));
        cardList.Add(new Card(9, "Trapeze Artist", 6, 2, 4, "Attacks over blocking units", Resources.Load<Sprite>("TrapezeArtist")));
        cardList.Add(new Card(10, "None", 0, 0, 0, "None", Resources.Load<Sprite>("placeHolder")));
        cardList.Add(new Card(11, "None", 0, 0, 0, "None", Resources.Load<Sprite>("placeHolder")));
        cardList.Add(new Card(12, "None", 0, 0, 0, "None", Resources.Load<Sprite>("placeHolder")));
        cardList.Add(new Card(13, "None", 0, 0, 0, "None", Resources.Load<Sprite>("placeHolder")));
        cardList.Add(new Card(14, "None", 0, 0, 0, "None", Resources.Load<Sprite>("placeHolder")));
        cardList.Add(new Card(15, "None", 0, 0, 0, "None", Resources.Load<Sprite>("placeHolder")));
        cardList.Add(new Card(16, "None", 0, 0, 0, "None", Resources.Load<Sprite>("placeHolder")));
        cardList.Add(new Card(17, "None", 0, 0, 0, "None", Resources.Load<Sprite>("placeHolder")));
        cardList.Add(new Card(18, "None", 0, 0, 0, "None", Resources.Load<Sprite>("placeHolder")));
        cardList.Add(new Card(19, "None", 0, 0, 0, "None", Resources.Load<Sprite>("placeHolder")));
    }

    // Method to get a card from the database by its ID
    public static Card GetCardById(int cardId)
    {
        // Loop through the cardList to find the card with the specified ID
        foreach (Card card in cardList)
        {
            if (card.id == cardId)
            {
                // Return the card if found
                return card;
            }
        }
        // Return null if no card with the specified ID is found
        return null;
    }
}