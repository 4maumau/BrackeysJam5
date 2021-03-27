using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{
    private Vector2 target;
    private string targetTag;
    [SerializeField] private float speed = 10f;
    [SerializeField] private int  damage = 1;

    public GameObject explosionPrefab;

    Vector2 direction;

    public void OnObjectSpawn()
    {
        Invoke("DeactivateObject", 3f);

        
    }

    void Update()
    {
               
        float distanceThisFrame = speed * Time.deltaTime;

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

            ScreenShakeController.instance.AddTrauma(.1f);
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosion, 1);
            //Destroy(gameObject);
            DeactivateObject();
        }
    }

    private void DeactivateObject()
    {
        gameObject.SetActive(false);
    }
}
