using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyDeath : MonoBehaviour
{
    
    [SerializeField] private GameObject eggPrefab;
        
    public void OnDeath()
    {
        // play death animation
        if (Random.value > 0.73)
            Instantiate(eggPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
