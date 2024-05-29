using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControllers : MonoBehaviour
{
    
    public void NewGameNormal()
    {
        SceneManager.LoadScene("Normal");
    }

    public void NewGameHard()
    {
        SceneManager.LoadScene("Hard");
    }

    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Game quit");
    }
}
