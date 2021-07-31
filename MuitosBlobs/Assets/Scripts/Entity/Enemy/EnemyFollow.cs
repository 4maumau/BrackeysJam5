using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private Transform enemyManager;
    public bool isAlive = true;

    [SerializeField] private float speed = -5;
    [SerializeField] private float knockbackFadeTime = 2f;
    private Vector2 knockbackForce;


    [Header ("Bomb Enemy")]
    [SerializeField] private float maxSpeed = 5;
    [SerializeField] private float steerStrength = 19;
    [HideInInspector]public Transform target;

    Vector2 position;
    Vector2 velocity;
    Vector2 desiredDirection;

    private Animator animator;
    

    [SerializeField] private bool isBombEnemy = false;
    private Bomb bombChild;
    private EnemyDeath enemyDeath;

   

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.25f);
        

        transform.parent = enemyManager;
        animator = GetComponentInChildren<Animator>();

        bombChild = GetComponentInChildren<Bomb>();
        enemyDeath = GetComponentInChildren<EnemyDeath>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            if (isAlive) animator.Play("DinoIdle");
        }
        else
        {
            if (isAlive)
            {
                animator.Play("DinoRun");
                if (isBombEnemy)
                    BombMove();
                else
                    RangedMove();
            }
        }

        KnockbackMove();
    }

    private void RangedMove()
    {
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (!isBombEnemy)
        {
            if (distanceToTarget > 5f)
            {
                speed = 5f;
            }
            else speed = -4f;
        }

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    private void BombMove()
    {
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (!isBombEnemy && distanceToTarget < 5f)// mas ranged enemy run away when too close
            desiredDirection = (position - (Vector2)target.position).normalized;
        else
            desiredDirection = ((Vector2)target.position - position).normalized;

        Vector2 desiredVelocity = desiredDirection * maxSpeed;
        Vector2 desiredSteeringForce = (desiredVelocity - velocity) * steerStrength;
        Vector2 acceleration = Vector2.ClampMagnitude(desiredSteeringForce, steerStrength) / 1;

        velocity = Vector2.ClampMagnitude(velocity + acceleration * Time.deltaTime, maxSpeed);
        position += velocity * Time.deltaTime;

        
        knockbackForce = Vector2.Lerp(knockbackForce, Vector2.zero, knockbackFadeTime * Time.deltaTime);
        position += (knockbackForce * Time.deltaTime);

        transform.position = Vector2.MoveTowards(transform.position, position, maxSpeed * Time.deltaTime);
    }


    private void KnockbackMove()
    {

        knockbackForce = Vector2.Lerp(knockbackForce, Vector2.zero, knockbackFadeTime * Time.deltaTime);
        transform.position += (Vector3) knockbackForce * Time.deltaTime;
    }

    public void AddKnockbackForce(Vector2 _knockbackForce)
    {
        knockbackForce = _knockbackForce;
    }

    void UpdateTarget()
    {
        GameObject[] chickens = GameObject.FindGameObjectsWithTag("Chicken");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestChicken = null;
        foreach (GameObject chicken in chickens)
        {
            float distanceToChicken = Vector3.Distance(transform.position, chicken.transform.position);
            if (distanceToChicken < shortestDistance)
            {
                shortestDistance = distanceToChicken;
                nearestChicken = chicken;
            }
        }

        if (nearestChicken != null)
        {
            target = nearestChicken.transform;
        }
        else
        {
            target = null;
        }
    }
}
