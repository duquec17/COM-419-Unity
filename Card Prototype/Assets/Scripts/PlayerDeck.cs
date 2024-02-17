using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerDeck : MonoBehaviour
{

    public int x;
    public static int deckSize;
    public List<Card> deck = new List<Card>();
    public static List<Card> staticDeck = new List<Card>();

    public GameObject cardInDeck1;
    public GameObject cardInDeck2;
    public GameObject cardInDeck3;
    public GameObject cardInDeck4;

    public GameObject CardToHand;
    public GameObject[] Clones;
    public GameObject Hand;
    // Start is called before the first frame update
    void Start()
    {
        x = 0;
        deckSize = 10;

        for (int i = 0; i < 10; i++)
        {
            //x = Random.Range(1, 10);
            deck[i] = CardDatabase.cardList[i];
        }

        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {

        staticDeck = deck;

        if (deckSize < 8)
        {
            cardInDeck1.SetActive(false);
        }
        if (deckSize < 6)
        {
            cardInDeck2.SetActive(false);
        }
        if (deckSize < 4)
        {
            cardInDeck3.SetActive(false);
        }
        if (deckSize < 1)
        {
            cardInDeck4.SetActive(false);
        }

        if (TurnSystem.startTurn == true)
        {
            StartCoroutine(Draw(1));
            TurnSystem.startTurn = false;
        }
    }

    IEnumerator StartGame()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1);
            //NEW
            Instantiate(CardToHand, transform.position, transform.rotation);
        }

    }


    IEnumerator Draw(int x)
    {
        for (int i = 0; i < x; i++)
        {
            yield return new WaitForSeconds(1);
            Instantiate(CardToHand, transform.position, transform.rotation);
        }
    }

}