using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{

    [SerializeField] private float speed = 10f;
    [SerializeField] private int  damage = 1;

    private string targetTag;
    private Vector2 target;
    private Vector2 direction;
   
    public GameObject explosionPrefab;

    private bool notExploded = true;
    private SpriteRenderer spriteRenderer;
    private Sprite initialSprite;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialSprite = spriteRenderer.sprite;
    }


    public void OnObjectSpawn()
    {
        spriteRenderer.sprite = initialSprite;

        Invoke("DeactivateObject", 3f);

        notExploded = true;
    }

    void Update()
    {
               
        float distanceThisFrame = speed * Time.deltaTime;

        if(notExploded)
            transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        
    }

    public void Seek(Vector2 _target, string tag)
    {
        targetTag = tag;
        target = _target;

        if (target != null)
            direction = target - (Vector2)transform.position;

    }

    private void NotStart()
    {
        if (target != null)
            direction = target - (Vector2)transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == targetTag)
        {
            LifeManager lifeManager = collision.gameObject.GetComponent<LifeManager>();
            lifeManager.TakeDamage(damage);

            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosion, 1);
            notExploded = false;
            DeactivateObject();
        }
    }

    private void DeactivateObject()
    {
        gameObject.SetActive(false);
    }
}
