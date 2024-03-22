using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class HandManager : NetworkBehaviour
{
    public Transform handPanel; // Reference to the panel where cards will be displayed
    public GameObject cardPrefab; // Reference to the prefab for the card UI

    // Networked list to store card IDs in the hand
    public SyncList<int> handCardIds = new SyncList<int>();

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
            GameObject newCard = Instantiate(cardPrefab, handPanel);
            // Set up the card UI based on the cardId (e.g., retrieve card data from a card database)
        }
    }

    // Handle interactions with the cards in the hand (e.g., clicking or dragging)
    // Add methods for handling card interactions as needed

    // AddCardToHand method for local player (not used in networked version)
    public void AddCardToHand(Sprite cardSprite)
    {
        // Instantiate a new card prefab locally
        GameObject newCard = Instantiate(cardPrefab, handPanel);
        // Set the sprite of the card UI
        newCard.GetComponent<Image>().sprite = cardSprite;
    }


}
