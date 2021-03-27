using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathMenu : MonoBehaviour
{
    [Header("Texts")]
    public TextMeshProUGUI scoreTXT;
    public TextMeshProUGUI chickenTXT;
    public TextMeshProUGUI deathMenuTXT;



    void Start()
    {
        scoreTXT.SetText("Score:{}" , GameManager.instance.score);
        chickenTXT.SetText("Chickens Raised:{}" , GameManager.instance.totalChicken);
        deathMenuTXT.SetText("High Score:{}", PlayerPrefs.GetInt("HS"));
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
