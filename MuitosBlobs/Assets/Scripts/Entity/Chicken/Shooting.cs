using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private string bullletTag;

    [SerializeField] private string targetTag;

    [SerializeField] private float range = 10f;
    [SerializeField] private float fireRate = 1f;
    private float fireCountdown = 0f;
    private Transform target;

    Transform spawnPosition;

    private EntityAnimator entityAnimator;

    ObjectPooler objPooler;

    void Start()
    {
        objPooler = ObjectPooler.Instance;

        entityAnimator = GetComponentInChildren<EntityAnimator>();

        InvokeRepeating("UpdateTarget", 0f, 0.5f);

        if (targetTag == "Chicken") spawnPosition = transform.GetChild(1);
        else spawnPosition = transform;
    }

    // Update is called once per frame
    void Update()
    {
        FireCountdown();
    }

    private void FireCountdown()
    {
        if (fireCountdown <= 0 && target != null)
        {
            OptimalShoot();
            //Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }


    private void Shoot()
    {
        entityAnimator.DoSqueeze(1.34f, 0.65f, 0.06f);
                
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Seek(target.position, targetTag);
    }

    private void OptimalShoot()
    {
        GameObject bullet = objPooler.SpawnFromPool(bullletTag, spawnPosition.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Seek(target.position, targetTag);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(targetTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }
}
