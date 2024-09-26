using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardFlipper : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private CardManager manager;
    public AnimationCurve scaleCurve;
    public float duration = 0.5f;

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        manager = GetComponent<CardManager>();
    }

    public void FlipCard(Sprite startImg, Sprite endImg, int cardIndex) {
        StopCoroutine(Flip(startImg, endImg, cardIndex));
        StartCoroutine(Flip(startImg, endImg, cardIndex));
    }

    IEnumerator Flip(Sprite startImg, Sprite endImg, int cardIndex) {
        spriteRenderer.sprite = startImg;

        float time = 0f;
        while (time <= 1f) {
            float scale = scaleCurve.Evaluate(time);
            time = time + Time.deltaTime / duration;

            Vector3 localScale = transform.localScale;
            localScale.x = scale;
            transform.localScale = localScale;

            if (time >= 0.5f) {
                spriteRenderer.sprite = endImg;
            }

            yield return new WaitForFixedUpdate();
        }

        if (cardIndex == -1) {
            manager.ToggleFaceNoAnimation(false);
        } else {
            manager.cardIndex = cardIndex;
            manager.ToggleFaceNoAnimation(true);
        }
    }
}
