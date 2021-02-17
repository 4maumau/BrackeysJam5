using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    [SerializeField] private GameObject chickenPrefab;
    private Animator animator;
    

    private bool spawning = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Chicken"){
            if (!spawning)
            {
                spawning = true;
                animator.Play("EggSpawn");
            }
        }
    }

    public void SpawnChicken()
    {
        Instantiate(chickenPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    
}
