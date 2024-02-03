using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Card : MonoBehaviour
{
    //Variable list
    public int id;
    public int cost;
    public int power;
    public int health;
    public string cardDescription;
    public string cardName;   

    public Card()
    {

    }

  
    public Card(int ID, int Mana, int Attack, int HP, string CardName, string CardDes)
    {
        //Will pass variables Through with new names
        id = ID;
        cost = Mana;
        power = Attack;
        health = HP;
        cardName = CardName;
        cardDescription = CardDes;

    }
}
