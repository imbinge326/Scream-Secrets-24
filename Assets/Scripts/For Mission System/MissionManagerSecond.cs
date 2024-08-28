using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Include the UI namespace
using UnityEngine.SceneManagement; // Include the SceneManagement namespace

public class MissionManagerSecond : MonoBehaviour
{
    private List<Day> days = new List<Day>();
    private int currentDayIndex = 1;      // Index to track the current day
    private Day currentDay;               // Current day being processed

    public Text currentMissionsText;      // Reference to the UI Text component for current missions
    public Button nextDayButton;          // Reference to the UI Button for switching the day
    public int nextSceneIndex; // Add a public string for the scene name to load

    void Start()
    {
        // Initialize the days and missions
        InitializeMissions();
        StartDay(currentDayIndex);

        // Ensure the button is set up to call the TriggerNextDay method
        if (nextDayButton != null)
        {
            nextDayButton.onClick.AddListener(LoadNextScene);
            nextDayButton.gameObject.SetActive(false);  // Hide the button initially
        }
    }

    void Update()
    {
        // Allow the player to switch to the next day by pressing 'N'
        //if (Input.GetKeyDown(KeyCode.N))
        //{
        //if (CanSwitchDay())
        //{
        //SwitchToNextDay();
        //}
        //else
        //{
        //Debug.Log("Complete all missions before switching to the next day.");
        //}
        //}

        // Show the button if all missions are completed
        if (nextDayButton != null && CanSwitchDay())
        {
            nextDayButton.gameObject.SetActive(true);  // Show the button when all missions are completed
        }
        else if (nextDayButton != null)
        {
            nextDayButton.gameObject.SetActive(false); // Hide the button if not all missions are completed
        }
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
                missionName = "Talk To Stranger",
                description = "Time to face the real thing.",
                objectives = new List<string> { "Talk To Stranger" },
                isCompleted = false
            },
            new Mission
            {
                missionName = "Rub Some Balls",
                description = "You are too lonely, make friends with an inaminate object.",
                objectives = new List<string> { "Rub Some Balls" },
                isCompleted = false
            },
            new Mission
            {
                missionName = "Go To Bed",
                description = "You're tired so why not?",
                objectives = new List<string> { "Go To Bed" },
                isCompleted = false
            }
        });

        InitializeDay("Day 4", new List<Mission>
        {
            new Mission
            {
                missionName = "Talk To Stranger",
                description = "Time to face the real thing.",
                objectives = new List<string> { "Talk To Stranger" },
                isCompleted = false
            },
            new Mission
            {
                missionName = "Rub Some Balls",
                description = "You are too lonely, make friends with an inaminate object.",
                objectives = new List<string> { "Rub Some Balls" },
                isCompleted = false
            },
            new Mission
            {
                missionName = "Go To Bed",
                description = "You're tired so why not?",
                objectives = new List<string> { "Go To Bed" },
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
                // Display mission objectives.
            }
        }
    }

    public void CompleteMission(string missionName)
    {
        Debug.Log($"Attempting to complete mission: {missionName}");

        Mission mission = currentDay.missions.Find(m => m.missionName == missionName);
        if (mission != null && !mission.isCompleted)
        {
            mission.isCompleted = true;
            Debug.Log($"Mission '{mission.missionName}' completed.");

            // Check if all missions for the current day are completed
            if (CanSwitchDay())
            {
                Debug.Log($"All missions for {currentDay.dayName} completed.");
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
        // Check if the mission is part of the current day's missions and is not completed
        Mission mission = currentDay.missions.Find(m => m.missionName == missionName);
        return mission != null && !mission.isCompleted;
    }

    bool CanSwitchDay()
    {
        // Check if all missions for the current day are completed
        return currentDay.missions.TrueForAll(m => m.isCompleted);
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
        }
    }

    //To switch scenes manually
    void LoadNextScene()
    {
        if (CanSwitchDay()) // Ensure all missions for the current day are completed
        {
            if (nextSceneIndex >= 0 && nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                Debug.LogError("Next scene name is not set.");
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
            // Check if all missions for today are completed
            if (CanSwitchDay())
            {
                currentMissionsText.text = "All Missions Are Completed";
            }
            else
            {
                // Display missions that are not yet completed
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
}
