using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAnimator : MonoBehaviour
{
    private SpriteRenderer chickenSprite;
    public bool isEnemy;

    private EnemyFollow enemyFollow; 

    private void Start()
    {
        chickenSprite = GetComponent<SpriteRenderer>();
        if (isEnemy)
            enemyFollow = GetComponentInParent<EnemyFollow>();
    }

    private void Update()
    {
        FlipSprite();
    }

    private void FlipSprite()
    {
        Vector2 lookTo;
        if (!isEnemy)
            lookTo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        else
        {
            if (enemyFollow.target != null)
                lookTo = enemyFollow.target.position;
            else lookTo = Vector2.zero;
        }

        chickenSprite.flipX = lookTo.x < transform.position.x;
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
