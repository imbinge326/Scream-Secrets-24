using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintCheck : MonoBehaviour
{
    public GameObject doorDayOne, doorDayTwo, doorDayThree;
    public MissionManager missionManager;

    public void Start()
    {
        var currentDay = missionManager.currentDayIndex;

        switch (currentDay)
        {
            case 0:
            doorDayOne.SetActive(false);
            break;

            case 1:
            doorDayTwo.SetActive(false);
            break;

            case 2:
            doorDayThree.SetActive(false);
            break;

            default:
            break;
        }

    }
}
