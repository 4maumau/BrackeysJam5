using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherAnimator : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private SpriteRenderer bodyRenderer;
    [SerializeField] private SpriteRenderer shadowRenderer;

    void Start()
    {
        Sprite pickedSprite = sprites[Random.Range(0, sprites.Length)];

        bodyRenderer.sprite = pickedSprite;
        shadowRenderer.sprite = pickedSprite;

    }
      
}
