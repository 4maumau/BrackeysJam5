using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyDeath : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private string deathAnimation = "DinoDeath";
    private bool dropBomb = true;

    private void Start()
    {
        animator = GetComponent<Animator>(); 
    }

    public void OnDeath()
    {
        animator.Play(deathAnimation);
       
        GetComponentInParent<EnemyFollow>().isAlive = false;
    }

    public void OnBombEnemyAnimationEnd()
    {
        if (dropBomb)
        {
            Instantiate(dropPrefab, transform.position, Quaternion.identity);
            print("called prefab");
        }

        GameManager.instance.AddScore(30);
        Destroy(transform.root.gameObject);
    }

    public void DontDropBomb()
    {
        print("called dont drop bomb");
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
