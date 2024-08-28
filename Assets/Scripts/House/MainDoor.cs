using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainDoor : MonoBehaviour
{
    public MissionManager missionManager;
    public string dayOneScene, dayTwoScene, dayThreeScene;

    public void OnExitHouse()
    {
        var currentDay = missionManager.currentDayIndex;

        switch (currentDay)
        {
            case 0:
                SceneManager.LoadScene(dayOneScene);
                break;
            
            case 1:
                SceneManager.LoadScene(dayTwoScene);
                break;

            case 2:
                SceneManager.LoadScene(dayThreeScene);
                break;

            default:
                Debug.Log("Unavailable to go through");
                break;
        }
    }
}
