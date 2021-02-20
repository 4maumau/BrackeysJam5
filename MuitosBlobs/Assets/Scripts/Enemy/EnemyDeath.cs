using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyDeath : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private GameObject eggPrefab;

    private void Start()
    {
        animator = GetComponent<Animator>(); 
    }

    public void OnDeath()
    {
        animator.Play("DinoDeath");
    }

    public void OnDinoAnimationEnd()
    {
        if (Random.value > 0.61)
            Instantiate(eggPrefab, transform.position, Quaternion.identity);
        Destroy(transform.root.gameObject);
    }
}
