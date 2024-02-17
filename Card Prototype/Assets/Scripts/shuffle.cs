using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class shuffle : MonoBehaviour 
{


    public List<Card> deck = new List<Card>();
    public List<Card> container = new List<Card>();
    public int x;
    public int deckSize;

    void Start()
    {
        x=0;

        for(int i=0;i<40;i++)
        {
            x = Random.Range(1,4);
            //deck[i] = CardDatabase.cardList[x];
        }
    }

    void Update()
    {

    }

    public void Shuffle()
    {
        for(int i=0; i<deckSize; i++)
        {
            container[0] = deck[i];
            int randomIndex = Random.Range(i,deckSize);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = container[0];
        }
    }
}