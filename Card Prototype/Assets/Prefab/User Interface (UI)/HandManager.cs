using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class HandManager : NetworkBehaviour
{
    public Transform handPanel; // Reference to the panel where cards will be displayed
    public GameObject PlayerCard; // Reference to the prefab for the card UI

    // Networked list to store card IDs in the hand
    public readonly SyncList<int> handCardIds = new SyncList<int>(); // Marked as readonly

    // This method will be called on all clients when the handCardIds list changes
    private void OnHandCardIdsChanged(SyncList<int>.Operation op, int index, int oldItem, int newItem)
    {
        // Update the hand UI when the handCardIds list changes
        UpdateHandUI();
    }

    // Update the hand UI based on the handCardIds list
    void UpdateHandUI()
    {
        // Clear the hand panel
        foreach (Transform child in handPanel)
        {
            Destroy(child.gameObject);
        }

        // Instantiate card UI prefabs for each card ID in the hand
        foreach (int cardId in handCardIds)
        {
            // Find the card data based on the card ID from the card database
            Card cardData = CardDatabase.GetCardById(cardId);
            if (cardData != null)
            {
                GameObject newCard = Instantiate(PlayerCard, handPanel);
                // Set up the card UI based on the card data
                SetupCardUI(newCard, cardData);
            }
        }
    }

    // Set up the UI of a card based on its data
    void SetupCardUI(GameObject cardUI, Card cardData)
    {
        // Set the sprite of the card UI
        cardUI.GetComponent<Image>().sprite = cardData.spriteImage;
        // Set other card UI elements (name, cost, description, etc.) if needed
        // For example:
        // cardUI.GetComponent<CardUI>().SetName(cardData.cardName);
        // cardUI.GetComponent<CardUI>().SetCost(cardData.cost);
        // cardUI.GetComponent<CardUI>().SetDescription(cardData.cardDescription);
    }

    // Method to set up the player's initial hand
    public void SetupInitialHand()
    {
        // Randomly select cards from the database and add them to the player's hand
        for (int i = 0; i < 3; i++)
        {
            int randomCardId = Random.Range(0, CardDatabase.cardList.Count);
            handCardIds.Add(randomCardId);
        }
    }

    // Handle interactions with the cards in the hand (e.g., clicking or dragging)
    // Add methods for handling card interactions as needed

    // AddCardToHand method for local player (not used in networked version)
    public void AddCardToHand(Sprite cardSprite)
    {
        // Instantiate a new card prefab locally
        GameObject newCard = Instantiate(PlayerCard, handPanel);
        // Set the sprite of the card UI
        newCard.GetComponent<Image>().sprite = cardSprite;
    }


}
