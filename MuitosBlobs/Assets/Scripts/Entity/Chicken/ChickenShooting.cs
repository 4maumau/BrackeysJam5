using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenShooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    
    [SerializeField] private float fireRate = 1f;
    private float fireCountdown = 0f;

    private Vector2 mousePos;


    private EntityAnimator entityAnimator;

    void Start()
    {
        entityAnimator = GetComponentInChildren<EntityAnimator>();
    }

    
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        FireCountdown();        
    }


    private void FireCountdown()
    {
        if (fireCountdown <= 0 && Input.GetMouseButtonDown(0))
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    private void Shoot()
    {
        entityAnimator.DoSqueeze(1.34f, 0.65f, 0.06f);
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Seek(mousePos, "Enemy");
    }

}
