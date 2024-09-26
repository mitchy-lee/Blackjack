using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugChangeCard : MonoBehaviour
{
    private CardFlipper cardFlipper;
    private CardManager cardManager;
    private int cardIndex = 0;
    public GameObject card;

    void Awake() {
        cardManager = card.GetComponent<CardManager>();
        cardFlipper = card.GetComponent<CardFlipper>();
    }

    void OnGUI() {
        if (GUI.Button(new Rect(10,10,100,28), "Hit me!")) {
            if (cardIndex >= cardManager.cardFaces.Length) {
                cardIndex = 0;
                //cardManager.ToggleFace(false);
                cardFlipper.FlipCard(cardManager.cardFaces[cardManager.cardFaces.Length - 1], cardManager.cardBack, -1);
            } else {
                //cardManager.cardIndex = cardIndex;
                //cardManager.ToggleFace(true);

                if (cardIndex > 0) {
                    cardFlipper.FlipCard(cardManager.cardFaces[cardIndex - 1], cardManager.cardFaces[cardIndex], cardIndex);
                } else {
                    cardFlipper.FlipCard(cardManager.cardBack, cardManager.cardFaces[cardIndex], cardIndex);
                }

                cardIndex++;
            }
        }
    }
}
