using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Egg : MonoBehaviour
{
    [SerializeField] private GameObject chickenPrefab;
    private Animator animator;

    public UnityEvent OnStarterEggPickup;
    [SerializeField] private bool starterEgg;
    private bool spawning = false;

    private int points = 50;

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
                GameManager.instance.AddScore(points);

                if (starterEgg)
                {
                    FindObjectOfType<EnemySpawner>().StartSpawner();
                }
            }
        }
    }

    public void SpawnChicken()
    {
        Instantiate(chickenPrefab, transform.position, Quaternion.identity);
        Destroy(transform.parent.gameObject);
    }

    
}
