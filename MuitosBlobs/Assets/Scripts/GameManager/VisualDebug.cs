using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class VisualDebug : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    private bool isVisible = true;

    [Header("Texts")]
    public TextMeshProUGUI touchPos;

    [Space]
    public TextMeshProUGUI waveCounter;
    public TextMeshProUGUI hordeNumber;
    public TextMeshProUGUI timesToSpawn;
    public TextMeshProUGUI timeBetweenSpawns;

    private EnemySpawner enemySpawner;

    

    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector2 touchToWorldpos = Camera.main.ScreenToWorldPoint(touch.position);
            touchPos.text = "Touch Position : " + touchToWorldpos;
        }
        else
        {
            touchPos.text = "No touch contacts";
        }

        EnemySpawner.Wave thisWave = enemySpawner.currentWave;
        waveCounter.text = "Wave Index: " + (enemySpawner.waveCounter -1);
        hordeNumber.text = "Horde No: " + thisWave.hordeNumber;
        timesToSpawn.text = "X to Spawn: " + thisWave.timesToSpawn;
        timeBetweenSpawns.text = "S btwn Spawns: " + thisWave.timeBetweenSpawns + "s";
    }


    public void ToggleDebugVisibility(Toggle toogle)
    {
        if (isVisible)
        {
            canvasGroup.alpha = 0f; //this makes everything transparent
            canvasGroup.blocksRaycasts = false; //this prevents the UI element to receive input events
            isVisible = toogle.isOn;
        }
        else
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            isVisible = toogle.isOn;
        }
    }
}
