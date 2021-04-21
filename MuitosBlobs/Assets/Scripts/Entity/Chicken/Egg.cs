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
    [SerializeField] private bool goldenEgg = false;
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
                    OnStarterEggPickup?.Invoke();
                }
            }
        }
    }

    public void SpawnChicken()
    {
        if (goldenEgg)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine("GoldenSpawn");
        }
        else
        {
            Instantiate(chickenPrefab, transform.position, Quaternion.identity);
            Destroy(transform.parent.gameObject);   
        }
        
    }

    IEnumerator GoldenSpawn()
    {
        for (int i = 0; i < 10; i++)
        {
            Instantiate(chickenPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.07f);
        }

        Destroy(transform.parent.gameObject);
    }

    
}
