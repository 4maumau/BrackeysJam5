using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] entityPrefabs;
    private EnemySpawner enemySpawner;

    void Start()
    {
        enemySpawner = GetComponent<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(entityPrefabs[0], enemySpawner.GetSpawnPosition(), Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.W))
            Instantiate(entityPrefabs[1], enemySpawner.GetSpawnPosition(), Quaternion.identity);

        if (Input.GetKeyDown(KeyCode.E))
            Instantiate(entityPrefabs[2], enemySpawner.GetSpawnPosition(), Quaternion.identity);
        */
    }

 
}
