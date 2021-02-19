using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private float spawnRadius = 200;
    [SerializeField] private float timeBetweenSpawns = 5;
    [SerializeField] private float hordeSize = 6;
    [SerializeField] private GameObject enemyPrefab;
    
    
    void Start()
    {
        StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        while (true)
        {
            var angle = Random.Range(0, 360);
            var playerPosition = transform.position;
            var x = (float) (playerPosition.x + (spawnRadius * Math.Cos(angle)));
            var y = (float) (playerPosition.y + (spawnRadius * Math.Sin(angle)));

            for (var i = 0; i < hordeSize; i++)
            {
                Instantiate(enemyPrefab, new Vector3(x,y,playerPosition.z),Quaternion.identity);
                yield return new WaitForSeconds(0.5f);

            }
            
            yield return new WaitForSeconds(timeBetweenSpawns);
            
            
        }
    }

}
