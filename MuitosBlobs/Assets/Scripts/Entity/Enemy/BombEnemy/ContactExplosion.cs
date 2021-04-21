using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactExplosion : MonoBehaviour
{
    private EnemyDeath enemyDeath;
    private EnemyFollow enemyFollow;

    private Bomb bombChild;
    [SerializeField] private float explosionRange = 1f;
    

    private void Start()
    {
        enemyFollow = GetComponent<EnemyFollow>();
        enemyDeath = GetComponentInChildren<EnemyDeath>();


        bombChild = GetComponentInChildren<Bomb>();
    }

    private void Update()
    {
        // OldMethod();
    }

    private void OldMethod()
    {
        if (enemyFollow.target != null)
        {
            float distanceToTarget = Vector2.Distance(transform.position, enemyFollow.target.position);
            if (distanceToTarget < explosionRange && enemyFollow.isAlive)
            {
                ContactExplode();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Chicken")
            ContactExplode();
    }

    void ContactExplode()
    {
        enemyDeath.DontDropBomb();

        bombChild.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        bombChild.enabled = true;
        bombChild.dropGoldenEgg = false;
        bombChild.Explode();
        //bombChild.transform.parent = null;
    }

    private void OnDrawGizmos()
    {
         Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
