using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAnimator : MonoBehaviour
{
    private SpriteRenderer chickenSprite;

    private void Start()
    {
        chickenSprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        FlipSprite();
    }

    private void FlipSprite()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        chickenSprite.flipX = mouseWorldPos.x < transform.position.x;
    }

    public void DoSqueeze(float xSqueeze, float ySqueeze, float seconds)
    {
        StartCoroutine(JumpSqueeze(xSqueeze, ySqueeze, seconds));
    }

    IEnumerator JumpSqueeze(float xSqueeze, float ySqueeze, float seconds)
    {
        Vector3 originalSize = Vector3.one;
        Vector3 newSize = new Vector3(xSqueeze, ySqueeze, originalSize.z);
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            chickenSprite.transform.localScale = Vector3.Lerp(originalSize, newSize, t);
            yield return null;
        }
        t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            chickenSprite.transform.localScale = Vector3.Lerp(newSize, originalSize, t);
            yield return null;
        }

    }
}
