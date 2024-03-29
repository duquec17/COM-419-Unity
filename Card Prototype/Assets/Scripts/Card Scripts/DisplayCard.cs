using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class DisplayCard : MonoBehaviour
{

    public List<Card> displayCard = new List<Card>();
    public int displayId;

    public int id;
    public string cardName;
    public int cost;
    public int power;
    public int health;
    public string cardDescription;
    //public Sprite spriteImage;
    //public Image artImage;

    public TMP_Text nameText;
    public TMP_Text costText;
    public TMP_Text powerText;
    public TMP_Text healthText;
    public TMP_Text descriptionText;

    public bool cardBack;
    public static bool staticCardBack;

    public GameObject Hand;
    public int numberOfCardsInDeck;


    // Start is called before the first frame update
    void Start()
    {
        // numberOfCardsInDeck = PlayerDeck.deckSize;
        displayCard[0] = CardDatabase.cardList[displayId];

      
    }

    void Update()
    {
        id = displayCard[0].id;
        cardName = displayCard[0].cardName;
        cost = displayCard[0].cost;
        power = displayCard[0].power;
        health = displayCard[0].health;
        cardDescription = displayCard[0].cardDescription;
        //spriteImage = displayCard[0].spriteImage;
        //artImage.sprite = spriteImage;

        nameText.text = " " + cardName;
        costText.text = " " + cost;
        powerText.text = " " + power;
        descriptionText.text = " " + cardDescription;
        healthText.text = " " + health;

        staticCardBack = cardBack;
    }

    
}