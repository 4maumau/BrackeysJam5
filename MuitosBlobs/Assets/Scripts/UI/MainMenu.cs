using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    string mode;

    [Header("Texts")]
    public TextMeshProUGUI shootingModeTXT;
    public TextMeshProUGUI tipTXT;

    void Start()
    {
        if (PlayerPrefs.HasKey("ShootingMode"))
        {
            mode = PlayerPrefs.GetString("ShootingMode");
        }
        else
        {
            PlayerPrefs.SetString("ShootingMode", "Auto");
        }

        UpdateShootingMode();
    }

   
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    private void UpdateShootingMode()
    {
        if (PlayerPrefs.GetString("ShootingMode") == "Auto")
        {
            shootingModeTXT.text = "Auto";
            tipTXT.text = "(Recommended)";
        }
        else
        {
            shootingModeTXT.text = "Manual";
            tipTXT.text = "(Hardcore)";
        }
    }

    public void ChangeShootingMode()
    {
        if (PlayerPrefs.GetString("ShootingMode") == "Auto")
        {
            PlayerPrefs.SetString("ShootingMode", "Manual");
        }
        else if (PlayerPrefs.GetString("ShootingMode") == "Manual")
        {
            PlayerPrefs.SetString("ShootingMode", "Auto");
        }

        UpdateShootingMode();
    }
}
