using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class CardStackManager : MonoBehaviour
{
    private List<int> cards;

    public bool isGameDeck;

    public bool hasCards {
        get { return cards != null && cards.Count > 0; }
    }

    public event CardEventHandler CardRemoved;
    public event CardEventHandler CardAdded;

    public int cardCount {
        get {
            if (cards == null) {
                return 0;
            } else {
                return cards.Count;
            }
        }
    }

    public IEnumerable<int> GetCards() {
        foreach (int i in cards) {
            yield return i;
        }
    }

    public int Pop() {
        int tmp = cards[0];
        cards.RemoveAt(0);

        if (CardRemoved != null) {
            CardRemoved(this, new CardEventArgs(tmp));
        }

        return tmp;
    }

    public void Push(int card) {
        cards.Add(card);

        if (CardAdded != null) {
            CardAdded(this, new CardEventArgs(card));
        }
    }

    public int HandValue() {
        int total = 0;
        int aces = 0;

        foreach(int card in GetCards()) {
            int cardRank = card % 13;

            if (cardRank <= 8) { // number cards
                cardRank += 2;
                total += cardRank;
            } else if (cardRank > 8 && cardRank < 12) { // face cards
                cardRank = 10;
                total += cardRank;
            } else if (cardRank == 12) {
                aces++; // keep track of the number of aces, deal with total after
                total += 11;
            }

            while (total > 21 && aces > 0) {
                total -= 10;
                aces--;
            }
        }

        return total;
    }

    public void CreateDeck() {
        cards.Clear();

        for (int i = 0; i < 52; i++) {
            cards.Add(i);
        }

        int n = cards.Count;
        while (n > 1) {
            n--;
            int k = Random.Range(0, n + 1);
            int tmp = cards[k];
            cards[k] = cards[n];
            cards[n] = tmp;
        }
    }

    public void Reset() {
        cards.Clear();
    }

    void Awake() {
        cards = new List<int>();
        if (isGameDeck) {
            CreateDeck();
        }
    }
}
