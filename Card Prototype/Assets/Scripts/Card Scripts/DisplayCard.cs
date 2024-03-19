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
    public Sprite spriteImage;
    public Image artImage;

    public bool cardBack;
    public static bool staticCardBack;

    public GameObject Hand;
    public int numberOfCardsInDeck;


    // Start is called before the first frame update
    void Start()
    {
        // numberOfCardsInDeck = PlayerDeck.deckSize;
        displayCard[5] = CardDatabase.cardList[5];

        id = displayCard[5].id;
        cardName = displayCard[5].cardName;
        cost = displayCard[5].cost;
        power = displayCard[5].power;
        health = displayCard[5].health;
        cardDescription = displayCard[5].cardDescription;
        spriteImage = displayCard[5].spriteImage;
        artImage.sprite = spriteImage;
    }

    // Update is called once per frame
    void Update()
    {
    }
}