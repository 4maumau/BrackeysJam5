using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{

    public Wave[] waves;
    private Wave currentWave;
    private int waveCounter;


    [SerializeField] private float spawnRadius = 200;
    [SerializeField] private float timeBetweenSpawns = 5;
    [SerializeField] private int hordeSize = 5;
    [SerializeField] private GameObject enemyPrefab;

    
    
    void Start()
    {
        StartCoroutine(Spawner());
        
        waveCounter++;
        currentWave = waves[waveCounter - 1];
              
    }

    IEnumerator Spawner()
    {
        while (true)
        {
            

            SetWave();

            for (int i = 0; i < currentWave.hordeNumber; i++)
            {
                var angle = Random.Range(0, 360);
                var playerPosition = transform.position;
                var x = (float)(playerPosition.x + (spawnRadius * Math.Cos(angle)));
                var y = (float)(playerPosition.y + (spawnRadius * Math.Sin(angle)));

                for (int b = 0; b < hordeSize; b++)
                {
                    Instantiate(enemyPrefab, new Vector3(x,y,playerPosition.z),Quaternion.identity);
                    //yield return new WaitForSeconds(0.5f);
                }

                yield return new WaitForSeconds(0.5f);
            }
            

            
             currentWave.timesToSpawn--;
            
            yield return new WaitForSeconds(currentWave.timeBetweenSpawns);
            
            
        }
    }
    

    private void SetWave()
    {
        
        if (currentWave.timesToSpawn < 0)
        {
            waveCounter++;
            if (waveCounter - 1 < waves.Length)
                currentWave = waves[waveCounter - 1];
            else currentWave = waves[waves.Length - 1];
        }
    }

    [System.Serializable]
    public class Wave
    {
        public int hordeNumber;
        public float timeBetweenSpawns;
        public int timesToSpawn;
    }

}
