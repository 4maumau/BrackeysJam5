using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [Header("Texts")]
    public TextMeshProUGUI chickenTXT;
    public TextMeshProUGUI scoreTXT;
    
    public int totalChicken;
    public int score = 0;

    private int chickenCounter;
    private int highScore;

    [Header("UI")]
    public GameObject HUI;
    public GameObject deathMenu;

    public GameObject tutorial;
    public GameObject manualTut;


    public Texture2D autoCursor;
    public Texture2D manualCursor;


    public UnityEvent OnGameOver;

    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        if (PlayerPrefs.GetString("ShootingMode") == "Manual")
        {
            manualTut.SetActive(true);
        }
        Invoke("DeactivateTutorial", 15f);

        if (PlayerPrefs.HasKey("HS"))
        {
            highScore = PlayerPrefs.GetInt("HS");
        }
        else PlayerPrefs.SetInt("HS", highScore);

        SetCursor();
    }
        
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void SetCursor()
    {
        if (PlayerPrefs.GetString("ShootingMode") == "Auto")
        {
            Cursor.SetCursor(autoCursor, Vector2.zero, CursorMode.ForceSoftware);
        }
        else
        {
            Cursor.SetCursor(manualCursor, Vector2.zero, CursorMode.ForceSoftware);
        }
    }

    void DeactivateTutorial()
    {
        tutorial.SetActive(false);
    }

    public void ChickenDeath()
    {
        chickenCounter--;
        chickenTXT.text = chickenCounter.ToString();

        if (chickenCounter <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        HUI.SetActive(false);
        deathMenu.SetActive(true);
        OnGameOver?.Invoke();
    }

    public void NewChicken()
    {
        chickenCounter++;
        totalChicken++;
        chickenTXT.text = chickenCounter.ToString();
    }

    public void AddScore(int points)
    {
        score += points;
        scoreTXT.SetText("Score:{}", score);

        if (score > PlayerPrefs.GetInt("HS"))
        {
            PlayerPrefs.SetInt("HS", score);
        }
    }
}
