using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainDoor : MonoBehaviour
{
    public string endScene;
    public void OnExitHouse()
    {
        SceneManager.LoadScene(endScene);
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
