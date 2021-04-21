using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Animator animator;
    private CharacterAudio _audio;
    
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
    public bool dropGoldenEgg = true;
    [SerializeField] private GameObject goldenEggPrefab;

    private void Awake()
    {
        countdown = delay;
        particles = GetComponent<ReleaseParticles>();

        animator = GetComponent<Animator>();
        _audio = GetComponent<CharacterAudio>();
        
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
        animator.Play("BombExplosion");
        _audio.PlaySound("Explosion");

        Collider2D[] colliders = Physics2D.OverlapCircleAll
            (transform.position, explosionRadius);

        foreach (Collider2D nearbyObject in colliders)
        {
            
            if (nearbyObject.tag == "Chicken" || nearbyObject.tag == "Enemy")
            {
                LifeManager lifeManager = nearbyObject.GetComponentInChildren<LifeManager>();
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
            else if (nearbyObject.tag == "Egg" && Vector2.Distance(transform.position, nearbyObject.transform.position) <= instaDeathRadius) // só mata o ovo se estiver no raio de instadeath
            {
                Destroy(nearbyObject.transform.parent.gameObject);
            }
        }

        
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

    public void DestroySelf()
    {
        Destroy(gameObject);
        if (dropGoldenEgg && Random.value > 0.8)
        {
            Instantiate(goldenEggPrefab, transform.position, Quaternion.identity);
        }
    }

}
