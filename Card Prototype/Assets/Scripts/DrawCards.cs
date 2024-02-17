using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCards : MonoBehaviour
{
    public GameObject PlayerCard;
    public GameObject AllyHand;

    public void OnClick()
    {
        for(int i = 0; i < 3; i++)
        {
            GameObject card = Instantiate(PlayerCard, new Vector2(0,0), Quaternion.identity);
            card.transform.SetParent(AllyHand.transform, false);
            Debug.Log("Drawing card" + i);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
