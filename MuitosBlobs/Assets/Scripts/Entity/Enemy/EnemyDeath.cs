using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyDeath : MonoBehaviour
{
    private Animator animator;
    private EnemyFollow enemyFollow;
    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private string deathAnimation = "DinoDeath";
    private bool dropBomb = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyFollow = GetComponentInParent<EnemyFollow>();
    }

    public void OnDeath()
    {
        animator.Play(deathAnimation);
        ScreenShakeController.instance.AddTrauma(.08f);

        if (enemyFollow.isAlive)
        {
            AudioManager.instance.PlaySound("EnemyDeath");
            enemyFollow.isAlive = false;
        }

    }

    public void OnBombEnemyAnimationEnd()
    {
        if (dropBomb)
        {
            Instantiate(dropPrefab, transform.position, Quaternion.identity);
        }

        GameManager.instance.AddScore(30);
        Destroy(transform.root.gameObject);
    }

    public void DontDropBomb()
    {
        dropBomb = false;
    }

    public void OnDinoAnimationEnd()
    {
        if (Random.value > 0.63)
            Instantiate(dropPrefab, transform.position, Quaternion.identity);
        GameManager.instance.AddScore(10);
        Destroy(transform.root.gameObject);
    }
}
