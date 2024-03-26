using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

public class Card 
{
    public int id;
    public string cardName;
    public int cost;
    public int power;
    public int health;
    public string cardDescription;
    public Sprite spriteImage;
    public int owner; // New field to indicate the ownwer of the card

    
    public Card(){
    }

    public Card(int Id, string CardName, int Cost, int Power, int Health, string CardDescription)
    {
        id = Id;
        cardName = CardName;
        cost = Cost;
        power = Power;
        cardDescription = CardDescription;
        health = Health;
        //spriteImage = SpriteImage;
        //owner = Owner; // Default owner is empty
    }
}
