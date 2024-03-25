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
        foreach (Transform child in handPanel.transform)
        {
            Destroy(child.gameObject);
        }

        // Instantiate card UI prefabs for each card ID in the hand
        for (int i = 0; i < handCardIds.Count; i++)
        {
            // Find the card data based on the card ID from the card database
            Card cardData = CardDatabase.GetCardById(handCardIds[i]);
            if (cardData != null)
            {
                GameObject newCard = Instantiate(PlayerCard, handPanel);
                // Set up the card UI based on the card data
                SetupCardUI(newCard, cardData);

                // Determine the owner of the card
                int owner = cardData.owner;

                // Determine the index where the card should be placed in the hand panel
                int cardIndex = i;
                if (owner == 1 && !isLocalPlayer)
                {
                    // If the card is owned by Player 1 and the local player is not Player 1,
                    // place the card at the bottom of the hand panel
                    cardIndex = handCardIds.Count - 1 - i;
                }
                else if (owner == 2 && isLocalPlayer)
                {
                    // If the card is owned by Player 2 and the local player is Player 1,
                    // place the card at the top of the hand panel
                    cardIndex = i;
                }

                // Reposition the card in the hand panel based on its index
                newCard.transform.SetSiblingIndex(cardIndex);
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

    [Server]
    // Method to set up the player's initial hand
    public void SetupInitialHand(Player player)
    {
        Debug.Log("Setting up initial hand...");

        // Log to track which cards are added to the player's hand
        List<int> addedCardIds = new List<int>();

        // Randomly select cards from the database and add them to the player's hand
        for (int i = 0; i < 3; i++)
        {
            int randomCardId = Random.Range(0, CardDatabase.cardList.Count);
            
            // Create the card object
            Card card = CardDatabase.GetCardById(randomCardId);

            // Assign the card to the player's hand
            player.handCards.Add(card);

            // Log the added card ID
            addedCardIds.Add(randomCardId);
        }

        // Log the added card IDs
        Debug.Log("Cards added to player's hand: " + string.Join(", ", addedCardIds));

        Debug.Log("Initial hand setup complete.");
    }

    // Handle interactions with the cards in the hand (e.g., clicking or dragging)
    // Add methods for handling card interactions as needed

    // AddCardToHand method for local player (not used in networked version)
    [ClientRpc]
    public void AddCardToHand(Sprite cardSprite)
    {

    }

}
