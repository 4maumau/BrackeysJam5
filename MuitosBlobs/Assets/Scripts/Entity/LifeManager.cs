using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LifeManager : MonoBehaviour
{
    public int startHealth = 2;
    private int health;

    private bool invincible;
    private float invincibleCountdown;

    public UnityEvent OnDeathEvent;
    
    

    void Start()
    {
        health = startHealth; 
    }

    // Update is called once per frame
    void Update()
    {
        if (invincible)
        {
            invincibleCountdown -= Time.deltaTime;
            if (invincibleCountdown <= 0)
                invincible = false;

            
        }
    }

    public void TakeDamage(int damage)
    {
        if (!invincible)
        {
            health -= damage;
            if (health <= 0)
                Die();
        }
    }

    public void SetInvincibility(float seconds)
    {
        invincibleCountdown = seconds;
        invincible = true;
    }

    private void Die()
    {
        OnDeathEvent?.Invoke();
    }
}
