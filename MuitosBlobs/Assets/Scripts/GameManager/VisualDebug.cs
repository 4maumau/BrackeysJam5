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

        DisplayUI();
        GoToWave();
    }

    private void DisplayUI()
    {
        EnemySpawner.Wave thisWave = enemySpawner.currentWave;
        waveCounter.text = "Wave Index: " + (enemySpawner.waveCounter);
        hordeNumber.text = "Horde No: " + thisWave.hordeNumber;
        timesToSpawn.text = "X to Spawn: " + thisWave.timesToSpawn;
        timeBetweenSpawns.text = "S btwn Spawns: " + thisWave.timeBetweenSpawns + "s";
    }

    public void ToggleDebugVisibility(Toggle toogle)
    {
        if (isVisible)
        {
            isVisible = toogle.isOn;
            canvasGroup.alpha = 0f; //this makes everything transparent
            canvasGroup.blocksRaycasts = false; //this prevents the UI element to receive input events
        }
        else
        {
            isVisible = toogle.isOn;
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }
    }

    void GoToWave()
    {
        bool leftShift = false;
        

        if (Input.GetKey(KeyCode.LeftShift))
            leftShift = true;

        if (leftShift)
        {
            print("left shift");
            if (Input.GetKeyDown("0"))
            {
                enemySpawner.JumpToWave(0);
            }
            else if (Input.GetKeyDown("1"))
            {
                enemySpawner.JumpToWave(1);
            }
            else if (Input.GetKeyDown("2"))
            {
                enemySpawner.JumpToWave(2);
            }
            else if (Input.GetKeyDown("3"))
            {
                enemySpawner.JumpToWave(3);
            }
            else if (Input.GetKeyDown("4"))
            {
                enemySpawner.JumpToWave(4);
            }
            else if (Input.GetKeyDown("5"))
            {
                enemySpawner.JumpToWave(5);
            }
            else if (Input.GetKeyDown("6"))
            {
                enemySpawner.JumpToWave(6);
            }
            else if (Input.GetKeyDown("7"))
            {
                enemySpawner.JumpToWave(7);
            }
            else if (Input.GetKeyDown("8"))
            {
                enemySpawner.JumpToWave(8);
            }
            else if (Input.GetKeyDown("9"))
            {
                enemySpawner.JumpToWave(9);
            }
        }

    }
}
