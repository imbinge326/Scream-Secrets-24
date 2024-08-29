using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MissionManagerSecond : MonoBehaviour
{
    private List<Day> days = new List<Day>();
    private int currentDayIndex = 1; // Index to track the current day
    private Day currentDay; // Current day being processed

    public Text currentMissionsText; // Reference to the UI Text component for current missions
    public int nextSceneIndex; // Add a public int for the scene index to load
    public MissionManager missionManager; // Reference to the MissionManager instance

    public GameObject missionManagerObject; // Reference to the GameObject with MissionManager script
    public GameObject missionManager3Object; // Reference to the GameObject with MissionManager3 script

    void Start()
    {
        // Initialize the days and missions
        InitializeMissions();
        StartDay(currentDayIndex);
    }

    void Update()
    {
        // Optionally, use this for debug purposes or other functionalities
    }

    void InitializeMissions()
    {
        // Initialize missions for each day
        InitializeDay("Day 1", new List<Mission>
        {
            new Mission
            {
                missionName = "Go To Bed",
                description = "You're tired so why not?",
                objectives = new List<string> { "Sleep" },
                isCompleted = false
            }
        });

        InitializeDay("Day 2", new List<Mission>
        {
            new Mission
            {
                missionName = "Buy Any Canned Food",
                description = "Eat Can Food.",
                objectives = new List<string> { "Buy Any Canned Food" },
                isCompleted = false
            },
            new Mission
            {
                missionName = "Get Coffee",
                description = "You're tired so why not?",
                objectives = new List<string> { "Get Coffee" },
                isCompleted = false
            }
        });

        InitializeDay("Day 3", new List<Mission>
        {
            new Mission
            {
                missionName = "Go Back",
                description = "You're tired so why not?",
                objectives = new List<string> { "Go Back" },
                isCompleted = false
            }
        });
    }

    void InitializeDay(string dayName, List<Mission> missions)
    {
        Day day = new Day { dayName = dayName, missions = missions };
        days.Add(day);
    }

    void StartDay(int dayIndex)
    {
        if (dayIndex < days.Count)
        {
            currentDay = days[dayIndex];
            Debug.Log($"Starting day: {currentDay.dayName}");
            ActivateMissions(currentDay);
            UpdateMissionUI(); // Update the mission text on UI
        }
        else
        {
            Debug.Log("All days completed!");
        }
    }

    void ActivateMissions(Day day)
    {
        foreach (Mission mission in day.missions)
        {
            if (!mission.isCompleted)
            {
                Debug.Log($"Mission '{mission.missionName}' is now active.");
            }
        }
    }

    public void CompleteMission(string missionName)
    {
        Debug.Log($"Attempting to complete mission: {missionName}");

        Mission mission = currentDay.missions.Find(m => m.missionName == missionName);

        if (mission != null && !mission.isCompleted)
        {
            if (missionName == "Go Back" && (currentDay.missions.Count == 1 || CanSwitchDay()))
            {
                mission.isCompleted = true;
                Debug.Log($"Mission '{mission.missionName}' completed.");

                Debug.Log("Current mission statuses:");
                foreach (var m in currentDay.missions)
                {
                    Debug.Log($"Mission '{m.missionName}' - Completed: {m.isCompleted}");
                }

                if (CanSwitchDay())
                {
                    Debug.Log($"All missions for {currentDay.dayName} completed.");
                    HideAndShowMissionManagers();
                    SwitchToNextDay();
                }
            }
            else if (missionName == "Go Back")
            {
                Debug.Log("You must complete all other missions before going back.");
                return;
            }
            else
            {
                mission.isCompleted = true;
                Debug.Log($"Mission '{mission.missionName}' completed.");

                Debug.Log("Current mission statuses:");
                foreach (var m in currentDay.missions)
                {
                    Debug.Log($"Mission '{m.missionName}' - Completed: {m.isCompleted}");
                }

                if (CanSwitchDay())
                {
                    Debug.Log($"All missions for {currentDay.dayName} completed.");
                    HideAndShowMissionManagers();
                    SwitchToNextDay();
                }
            }
        }
        else if (mission == null)
        {
            Debug.LogError($"Mission '{missionName}' not found for the current day.");
        }
        else if (mission.isCompleted)
        {
            Debug.LogWarning($"Mission '{missionName}' was already completed.");
        }

        UpdateMissionUI(); // Update the mission text on UI after completing a mission
    }

    public bool IsMissionActive(string missionName)
    {
        Mission mission = currentDay.missions.Find(m => m.missionName == missionName);
        return mission != null && !mission.isCompleted;
    }

    bool CanSwitchDay()
    {
        return currentDay.missions.TrueForAll(m => m.isCompleted);
    }

    void HideAndShowMissionManagers()
    {
        if (missionManagerObject != null)
        {
            missionManagerObject.SetActive(false); // Hide the current MissionManager
        }

        if (missionManager3Object != null)
        {
            missionManager3Object.SetActive(true); // Show MissionManager3
        }
    }

    void SwitchToNextDay()
    {
        currentDayIndex++;
        if (currentDayIndex < days.Count)
        {
            StartDay(currentDayIndex);
        }
        else
        {
            Debug.Log("No more days left.");
            LoadNextScene();
        }
    }

    public void LoadNextScene()
    {
        Debug.Log("LoadNextScene method called.");
        if (CanSwitchDay())
        {
            Debug.Log($"Attempting to load scene with index: {nextSceneIndex}");
            if (nextSceneIndex >= 0 && nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                // Save the current day index to PlayerPrefs before switching scenes
                PlayerPrefs.SetInt("DayIndex", currentDayIndex);
                PlayerPrefs.Save();
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                Debug.LogError("Next scene index is out of range or not set properly.");
            }
        }
        else
        {
            Debug.Log("Complete all missions before switching to the next day.");
        }
    }

    void UpdateMissionUI()
    {
        if (currentMissionsText != null)
        {
            if (CanSwitchDay())
            {
                currentMissionsText.text = "All Missions Are Completed";
            }
            else
            {
                currentMissionsText.text = "Missions for Today:\n";
                foreach (Mission mission in currentDay.missions)
                {
                    if (!mission.isCompleted)
                    {
                        currentMissionsText.text += $"- {mission.missionName}\n";
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning("currentMissionsText is not assigned.");
        }
    }

    public void NotifyMissionManagerToSwitchDay()
    {
        if (missionManager != null)
        {
            missionManager.SwitchToNextDay();
        }
        else
        {
            Debug.LogError("MissionManager reference is not assigned.");
        }
    }
}
