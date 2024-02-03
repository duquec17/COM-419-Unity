using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    //Allows for card to global and extracted from anywhere in project
    public static List<Card> cardList = new List<Card>();

    void Awake()
    {
        //Adds card with numbers in order of ID, Mana, Attack, CardName, & CardDescription
        cardList.Add(new Card(0, 0, 0, 0, "None", "None"));

        //First Test Deck (Circus) - ID, Mana, Attack, HP, CardName, & CardDescription
        cardList.Add(new Card(0, 1, 1, 2, "Jester", "Increase attack by 1 for every other Jester on the field."));
        cardList.Add(new Card(1, 1, 1, 2, "Jester", "Increase attack by 1 for every other Jester on the field."));
        cardList.Add(new Card(2, 1, 1, 2, "Jester", "Increase attack by 1 for every other Jester on the field."));
        cardList.Add(new Card(3, 1, 1, 1, "Volunteer", "Blocks next attack regardless of lane placement."));
        cardList.Add(new Card(4, 4, 0, 4, "One-Man Circus", "Owner is immune to damage while card is on the field."));
        cardList.Add(new Card(5, 3, 1, 2, "Knife Thrower", "Damages adjacent opponent lanes when attacking."));
        cardList.Add(new Card(6, 3, 1, 4, "Psycho Clown", "Attack increase by 1 whenever an ally dies while this card is on the field."));
        cardList.Add(new Card(7, 8, 2, 2, "Tiger Tamer", "Add two 4/4 tigers to your hand."));
        cardList.Add(new Card(8, 6, 2, 4, "Tapezist", "This can attack the player directly."));
        cardList.Add(new Card(9, 10, 4, 4, "Ring Leader", "Increase all ally units HP by 2 when this is placed on the board."));

    }
}
