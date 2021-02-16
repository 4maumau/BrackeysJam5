using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private Transform enemyManager;
    [SerializeField] private float speed = -5;

    private Transform target;


    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);

        transform.parent = enemyManager;

    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (distanceToTarget > 5f)
        {
            speed = 5f;
        }
        else speed = -4f;

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
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
