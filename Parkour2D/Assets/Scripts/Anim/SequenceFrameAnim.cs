using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SequenceFrameAnim : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Image selfImage;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        selfImage = GetComponent<Image>();
    }

    private void Update() {
        selfImage.sprite = spriteRenderer.sprite;
    }
}
