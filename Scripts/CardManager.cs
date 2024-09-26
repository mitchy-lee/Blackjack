using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    private SpriteRenderer _renderer;
    public Sprite[] cardFaces;
    public Sprite[] cardBacks;
    public Sprite cardBack;
    private CardFlipper cardFlipper;
    public int cardIndex;
    public Button selectCardBackButton;
    public GameObject deck;


    public void CardBackSelected() {
        //Debug.Log("card selection clicked " + deck.GetComponent<CardStackView>().cardBack);
        cardBack = deck.GetComponent<CardStackView>().cardBack;
    }
    public void ChangeCardBack(Sprite sprite) {
        deck.GetComponent<CardStackView>().cardBack = sprite;

        if (sprite.name == "red_card_back") {
            cardBack = cardBacks[0];
        } else if (sprite.name == "blue_card_back") {
            cardBack = cardBacks[1];
        } else if (sprite.name == "green_card_back") {
            cardBack = cardBacks[2];
        } else if (sprite.name == "purple_card_back") {
            cardBack = cardBacks[3];
        } else if (sprite.name == "pink_card_back") {
            cardBack = cardBacks[4];
        } else if (sprite.name == "orange_card_back") {
            cardBack = cardBacks[5];
        }
    }

    public void ToggleFace(bool showFace) {
        if (showFace) {
            // show card face
            cardFlipper.FlipCard(cardBack, cardFaces[cardIndex], cardIndex);
        } else {
            // show card back
            cardFlipper.FlipCard(cardBack, cardFaces[cardIndex], cardIndex);
        }
    }

    public void ToggleFaceNoAnimation(bool showFace) {
        if (showFace) {
            // show card face
            _renderer.sprite = cardFaces[cardIndex];
        } else {
            // show card back
            _renderer.sprite = cardBack;
        }
    }

    void Awake() {
        _renderer = GetComponent<SpriteRenderer>();
        cardFlipper = GetComponent<CardFlipper>();

        selectCardBackButton.onClick.AddListener(CardBackSelected);
    }

    // void Start() {
        
    // }
}
