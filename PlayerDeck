using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{
    
    public int x;
    public int deckSize;
    public List<Card> deck = new List<Card>();

    public GameObject cardInDeck1;
    public GameObject cardInDeck2;
    public GameObject cardInDeck3;
    public GameObject cardInDeck4;
    // Start is called before the first frame update
    void Start()
    {
        x = 0;

        for(int i = 0; i < 10; i++){
            x = Random.Range(1, 10);
            deck[i] = CardDatabase.cardList[x];
        }

    }

    // Update is called once per frame
    void Update()
    {
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
    }
}
