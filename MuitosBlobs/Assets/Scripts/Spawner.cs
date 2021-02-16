using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject chickenPrefab;

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(chickenPrefab, new Vector2(-13, 7), Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.W))
            Instantiate(enemyPrefab, new Vector2(13, -7), Quaternion.identity);
    }
}
