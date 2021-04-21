using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject bombEnemyPrefab;

    public Wave[] waves;
    [HideInInspector]public Wave currentWave;
    public int waveCounter;



    [SerializeField] private float spawnRadius = 200;
    [SerializeField] private int hordeSize = 5;
   

    private bool hasStarted = false;
    
    void Start()
    {
        waveCounter = 0;
        currentWave = waves[waveCounter];

        Invoke("StartSpawner", 10f);

        GameManager.instance.OnGameOver.AddListener(StopSpawner);
    }

    

    public void StartSpawner()
    {
        if (!hasStarted)
        {
            StartCoroutine(Spawner());

            hasStarted = true;
        }
    }

    public void StopSpawner()
    {
        StopCoroutine("Spawner");
    }

    IEnumerator Spawner()
    {
        yield return new WaitForSeconds(5);

        while (true)
        {
            SetWave();


            // spawns the number of hordes in the wave
            for (int i = 0; i < currentWave.hordeNumber; i++)
            {
                Vector2 spawnPosition = GetSpawnPosition();

                //spawn the horde
                for (int b = 0; b < hordeSize; b++)
                {
                    Instantiate(enemyPrefab, new Vector3(spawnPosition.x,spawnPosition.y,transform.position.z),Quaternion.identity);
                    //yield return new WaitForSeconds(0.5f);
                }
                yield return new WaitForSeconds(0.5f);
            }
            // spawns bomb enemies; 
            for (int i = 0; i < currentWave.bombEnemiesInWave; i++)
            {
                Vector2 spawnPosition = GetSpawnPosition();
                Instantiate(bombEnemyPrefab, new Vector3(spawnPosition.x, spawnPosition.y, transform.position.z), Quaternion.identity);
                
                yield return new WaitForSeconds(currentWave.timeBetweenBombEnemies);
            }

            currentWave.timesToSpawn--;
            
            yield return new WaitForSeconds(currentWave.timeBetweenSpawns);
        }
    }
   
    public Vector2 GetSpawnPosition()
    {
        float angle = Random.Range(0, 360);

        float x = (float)(transform.position.x + (spawnRadius * Math.Cos(angle)));
        float y = (float)(transform.position.y + (spawnRadius * Math.Sin(angle)));

        return new Vector2(x,y);
    }


    private void SetWave()
    {

        if (currentWave.timesToSpawn < 0)
        {
            waveCounter++;
            if (waveCounter < waves.Length)
                currentWave = waves[waveCounter];
            else currentWave = waves[waves.Length - 1];
        }

        if (currentWave.timesToSpawn < -5)
        {
            currentWave.timeBetweenSpawns = 8;
        }
       
    }

    public void JumpToWave(int waveIndex)
    {
        currentWave = waves[waveIndex];
        waveCounter = waveIndex;
    }

    [System.Serializable]
    public class Wave
    {
        [Tooltip("Hordes to spawn in the section")]
        public int hordeNumber;

        [Tooltip("Bomb Enemies to spawn each spawn 'section'.")]
        public int bombEnemiesInWave;
        public float timeBetweenBombEnemies = .5f;

        [Tooltip("Waiting time for next spawn section")]
        public float timeBetweenSpawns;
        [Tooltip("Number of times this wave will be spawned")]
        public int timesToSpawn;
    }

}
