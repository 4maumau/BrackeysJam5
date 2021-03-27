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
        if (enemyFollow.target != null)
        {
            float distanceToTarget = Vector2.Distance(transform.position, enemyFollow.target.position);
            if (distanceToTarget < explosionRange && enemyFollow.isAlive)
            {
                ContactExplode();
            }
        }
    }

    void ContactExplode()
    {
        enemyDeath.DontDropBomb();
        bombChild.enabled = true;
        bombChild.Explode();
    }

    private void OnDrawGizmos()
    {
         Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
