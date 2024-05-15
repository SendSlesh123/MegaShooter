using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void OnClickStart()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void OnClickExit()
    {
        Application.Quit();
    }

    public void ClearLevel()
    {
        PlayerPrefs.SetInt("Level", 1);
    }
}
