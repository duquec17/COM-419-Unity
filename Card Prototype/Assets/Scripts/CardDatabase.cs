using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CardDatabase : MonoBehaviour
{

    public static List<Card> cardList = new List<Card>();

    public int displayId;

    public int id;
    public string cardName;
    public int cost;
    public int power;
    public int health;
    public string cardDescription;
    public Sprite spriteImage;

    public TMP_Text nameText;
    public TMP_Text costText;
    public TMP_Text powerText;
    public TMP_Text descriptionText;
    public TMP_Text healthText;
    public Image artImage;

    public bool cardBack;
    public static bool staticCardBack;

    private void Awake()
    {
        cardList.Add(new Card(0, "Example", 0, 0, 0, "broke", spriteImage));
    }


    // Start is called before the first frame update
    void Start()
    {
        displayId = 0;
        id = cardList[0].id;
        cardName = cardList[0].cardName;
        cost = cardList[0].cost;
        power = cardList[0].power;
        health = cardList[0].health;
        cardDescription = cardList[0].cardDescription;
        spriteImage = cardList[0].spriteImage;


        nameText.text = " " + cardName;
        costText.text = " " + cost;
        powerText.text = " " + power;
        healthText.text = " " + health;
        descriptionText.text = " " + cardDescription;
        artImage.sprite = spriteImage;
    }


    // Update is called once per frame
    void Update()
    {
        
        staticCardBack = cardBack;

    }
}
