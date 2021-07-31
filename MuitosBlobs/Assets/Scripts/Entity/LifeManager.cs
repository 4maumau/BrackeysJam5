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

    private SpriteRenderer bodySprite;
    [SerializeField] private Color hurtColor;
    [SerializeField] private float flashTime = 0.1f;

    void Start()
    {
        health = startHealth;

        bodySprite = GetComponentInChildren<SpriteRenderer>();
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
            else StartCoroutine("FlashHit");
        }
    }

    IEnumerator FlashHit()
    {
        bodySprite.color = hurtColor;
        yield return new WaitForSeconds(flashTime);
        bodySprite.color = Color.white;
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
