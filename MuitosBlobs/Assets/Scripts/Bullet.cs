using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 target;
    private string targetTag;
    [SerializeField] private float speed = 10f;
    [SerializeField] private int  damage = 1;

    Vector2 direction;

    void Start()
    {
        Destroy(gameObject, 5f);
        if (target != null)
            direction = target - (Vector2) transform.position;
    }

    // Update is called once per frame
    void Update()
    {
               
        float distanceThisFrame = speed * Time.deltaTime;

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        // transform.position += Vector3.right * 10 * Time.deltaTime;
    }

    public void Seek(Vector2 _target, string tag)
    {
        targetTag = tag;
        target = _target;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == targetTag)
        {
            LifeManager lifeManager = collision.gameObject.GetComponent<LifeManager>();
            lifeManager.TakeDamage(damage);

            ScreenShakeController.instance.AddTrauma(.08f);
            //plays explosion particle
            Destroy(gameObject);
        }
    }
}
