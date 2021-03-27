using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float instaDeathRadius = 2f;

    [Range(0f,1f)]
    [SerializeField] private float screenShakePower = .75f;
    private ReleaseParticles particles;

    [SerializeField] private float xKnockbackForce = 5f;
    [SerializeField] private float yKnockbackForce = 15f;
    [SerializeField] private float enemyKnockbackForce = 20f;

    [SerializeField]private float delay = 3f;
    private float countdown;

    private bool hasExploded = false;

    private void Awake()
    {
        countdown = delay;
        particles = GetComponent<ReleaseParticles>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            Explode();

        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    public void Explode()
    {
        particles.EmitParticles();
        ScreenShakeController.instance.AddTrauma(screenShakePower);


        Collider2D[] colliders = Physics2D.OverlapCircleAll
            (transform.position, explosionRadius);

        foreach (Collider2D nearbyObject in colliders)
        {
            if (nearbyObject.tag == "Chicken" || nearbyObject.tag == "Enemy")
            {
                LifeManager lifeManager = nearbyObject.GetComponent<LifeManager>();
                if (Vector2.Distance(transform.position, nearbyObject.transform.position) <= instaDeathRadius) // deveria dar dano nos inimigos tb?
                {
                    lifeManager.TakeDamage(10);
                } 
                else
                {
                    lifeManager.TakeDamage(1);
                    Vector2 knockbackDirection = (nearbyObject.transform.position - transform.position).normalized;

                    if (nearbyObject.tag == "Chicken")
                        KnockbackChicken(nearbyObject, knockbackDirection);

                    else if (nearbyObject.tag == "Enemy")
                        KnockbackEnemy(nearbyObject, knockbackDirection);
                    
                }
            }
            else if (nearbyObject.tag == "Egg")
            {
                Destroy(nearbyObject.transform.parent.gameObject);
            }
        }

        //Destroy(gameObject);
    }

    private void KnockbackEnemy(Collider2D collision, Vector2 direction)
    {
        collision.GetComponent<EnemyFollow>().AddKnockbackForce(direction * enemyKnockbackForce);
    }

    private void KnockbackChicken(Collider2D collision, Vector2 direction)
    {
        Chicken chickenScript = collision.GetComponentInParent<Chicken>();
        chickenScript.JumpTo(direction * xKnockbackForce, yKnockbackForce);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, instaDeathRadius);
    }

}
