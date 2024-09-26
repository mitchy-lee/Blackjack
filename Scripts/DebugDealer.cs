using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDealer : MonoBehaviour
{
    public CardStackManager dealer;
    public CardStackManager player;
    
    // used for debugging of ace value:
    // private int count = 0;
    // private int[] cards = new int[] {9, 7, 12};

    void OnGUI() {
        if (GUI.Button(new Rect(10, 10, 256, 28), "Hit me!")) {
            player.Push(dealer.Pop());
        }

        // if (GUI.Button(new Rect(10, 10, 256, 28), "Hit me!")) {
        //     player.Push(cards[count++]);
        // }
    }
}
