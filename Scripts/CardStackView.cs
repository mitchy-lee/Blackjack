using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

[RequireComponent(typeof(CardStackManager))]
public class CardStackView : MonoBehaviour
{
    private CardStackManager deck;
    Dictionary<int, CardView> fetchedCards;
    public Vector3 start;
    public float cardOffset;
    public bool faceUp = false;
    public bool reverseLayerOrder = false;
    public GameObject cardPrefab;
    public bool isVisible = false;
    public Sprite cardBack;

    public void Toggle(int card, bool isFaceUp) {
        fetchedCards[card].IsFaceUp = isFaceUp;
    }

    public void ClearHand() {
        //isVisible = false;

        deck.Reset();

        foreach (CardView view in fetchedCards.Values) {
            Destroy(view.Card);
        }

        fetchedCards.Clear();
    }

    void Awake() {
        fetchedCards = new Dictionary<int, CardView>();
        deck = GetComponent<CardStackManager>();
        
        //if (isVisible) {
            ShowCards();
        //}

        deck.CardRemoved += Deck_CardRemoved;
        deck.CardAdded += Deck_CardAdded;
    }

    void Deck_CardAdded(object sender, CardEventArgs e) {
        float co = cardOffset * deck.cardCount;
        Vector3 tmp = start + new Vector3(co, 0f);
        AddCard(tmp, e.CardIndex, deck.cardCount, true);
    }

    void Deck_CardRemoved(object sender, CardEventArgs e) {
        if (fetchedCards.ContainsKey(e.CardIndex)) {
            Destroy(fetchedCards[e.CardIndex].Card);
            fetchedCards.Remove(e.CardIndex);
        }
    }

    void Update() {
        ShowCards();
    }

    // public void makeVisible() {
    //     isVisible = true;
    // }

    // public void makeNotVisible() {
    //     isVisible = false;
    // }

    public void ShowCards() {
        //if (isVisible) {
            int cardCount = 0;

            if (deck.hasCards) {
                foreach(int i in deck.GetCards()) {
                    float co = cardOffset * cardCount;
                    Vector3 tmp = start + new Vector3(co, 0f);
                    AddCard(tmp, i, cardCount);

                    cardCount++;
                }
            }
        //}
    }

    void AddCard(Vector3 position, int cardIndex, int positionalIndex, bool doAnimation = false) {
        if (fetchedCards.ContainsKey(cardIndex)) {
            if (!faceUp) {
                CardManager manager = fetchedCards[cardIndex].Card.GetComponent<CardManager>();
                if (doAnimation) {
                    manager.ToggleFace(fetchedCards[cardIndex].IsFaceUp);
                } else {
                    manager.ToggleFaceNoAnimation(fetchedCards[cardIndex].IsFaceUp);
                }
            }

            return;
        }

        GameObject cardCopy = (GameObject)Instantiate(cardPrefab, GameObject.Find("Deck").transform);
        cardCopy.transform.position = position;

        CardManager cardManager = cardCopy.GetComponent<CardManager>();
        cardManager.cardIndex = cardIndex;
        if (doAnimation) {
            cardManager.ToggleFace(faceUp);
        } else {
            cardManager.ToggleFaceNoAnimation(faceUp);
        }

        SpriteRenderer spriteRenderer = cardCopy.GetComponent<SpriteRenderer>();
        if (reverseLayerOrder) {
            spriteRenderer.sortingOrder = 51 - positionalIndex;
        } else {
            spriteRenderer.sortingOrder = positionalIndex;
        }

        fetchedCards.Add(cardIndex, new CardView(cardCopy));
    }
}
