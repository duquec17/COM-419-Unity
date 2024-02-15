using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardDisplay : MonoBehaviour
{

    public Card card;

    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public TMP_Text healthText;

    public TMP_Text manaText;
    public TMP_Text attackText;


    // Start is called before the first frame update
    void Start()
    {
        nameText.text = card.cardName;
        descriptionText.text = card.cardDescription;
        

        manaText.text = card.cost.ToString();
        attackText.text = card.power.ToString();
        healthText.text = card.health.ToString();
    }

}
