using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    int dealersFirstCard = -1;
    public CardStackManager player;
    public CardStackManager dealer;
    public CardStackManager deck;
    public Button hitButton;
    public Button standButton;
    public Button playAgainButton;
    public TextMeshProUGUI winnerText;
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI dealerScoreText;
    public TextMeshProUGUI bustText;
    public Button mainMenuButton;

    public void Hit() {
        mainMenuButton.interactable = false;

        player.Push(deck.Pop());
        if (player.HandValue() > 21) { // player busts
            hitButton.interactable = false;
            standButton.interactable = false;

            bustText.text = "Bust!";

            StartCoroutine(DealerTurn());
        }

        playerScoreText.text = "Score: " + player.HandValue();
    }

    public void Stand() {
        hitButton.interactable = false;
        standButton.interactable = false;

        StartCoroutine(DealerTurn());
    }

    public void PlayAgain() {
        playAgainButton.interactable = false;

        player.GetComponent<CardStackView>().ClearHand();
        dealer.GetComponent<CardStackView>().ClearHand();
        
        deck.GetComponent<CardStackView>().ClearHand();
        deck.CreateDeck();

        hitButton.interactable = true;
        standButton.interactable = true;

        dealersFirstCard = -1;

        winnerText.text = "";
        playerScoreText.text = "";
        dealerScoreText.text = "";
        bustText.text = "";

        StartGame();
    }

    void Start() {
        // start game on button press
        //StartGame();
    }

    public void StartGame() {
        for (int i=0; i<2; i++) {
            player.Push(deck.Pop());
            HitDealer();
        }

        playerScoreText.text = "Score: " + player.HandValue();
    }

    void HitDealer() {
        int card = deck.Pop();

        if (dealersFirstCard < 0) {
            dealersFirstCard = card;
        }

        dealer.Push(card);
        if (dealer.cardCount >= 2) {
            CardStackView view = dealer.GetComponent<CardStackView>();
            view.Toggle(card, true);
        }
    }

    IEnumerator DealerTurn() {
        hitButton.interactable = false;
        standButton.interactable = false;

        dealerScoreText.text = "Score: " + dealer.HandValue();

        CardStackView view = dealer.GetComponent<CardStackView>();
        view.Toggle(dealersFirstCard, true);
        view.ShowCards();
        yield return new WaitForSeconds(1f);

        while (dealer.HandValue() < 17) {
            HitDealer();
            dealerScoreText.text = "Score: " + dealer.HandValue();
            yield return new WaitForSeconds(1f);
        }

        if (player.HandValue() > 21 || (dealer.HandValue() > player.HandValue() && dealer.HandValue() <= 21)) {
            winnerText.text = "Dealer wins!";
        } else if (dealer.HandValue() > 21 || (player.HandValue() <= 21 && player.HandValue() > dealer.HandValue())) {
            winnerText.text = "You win!";
        } else if (dealer.HandValue() == player.HandValue()) {
            winnerText.text = "Push";
        } else {
            winnerText.text = "No Winner";
        }

        yield return new WaitForSeconds(1f);
        playAgainButton.interactable = true;
        mainMenuButton.interactable = true;
    }
}
