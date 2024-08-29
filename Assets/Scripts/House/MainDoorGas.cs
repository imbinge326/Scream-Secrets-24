using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainDoorGas : MonoBehaviour
{
    public MissionManagerThird missionManager;
    public string dayOneScene, dayTwoScene, dayThreeScene;

    public void OnExitHouse()
    {
        if (missionManager != null)
        {
            var currentDay = missionManager.currentDayIndex;

            switch (currentDay)
            {
                case 1: // Day 2
                    SceneManager.LoadScene(dayOneScene);
                    break;

                case 2: // Day 3
                    SceneManager.LoadScene(dayThreeScene);
                    break;

                default:
                    Debug.Log("Unavailable to go through");
                    break;
            }
        }
        else
        {
            Debug.LogError("MissionManager reference is not assigned in MainDoor script.");
        }
    }
}
