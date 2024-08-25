using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    private List<Day> days = new List<Day>();
    private int currentDayIndex = 0;      // Index to track the current day
    private Day currentDay;               // Current day being processed

    void Start()
    {
        // Initialize the days and missions
        InitializeMissions();
        StartDay(currentDayIndex);
    }

    void Update()
    {
        // Allow the player to switch to the next day by pressing 'N'
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (CanSwitchDay())
            {
                SwitchToNextDay();
            }
            else
            {
                Debug.Log("Complete all missions before switching to the next day.");
            }
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

    //To switch days manually
    public void TriggerNextDay()
    {
        if (CanSwitchDay())
        {
            SwitchToNextDay();
        }
        else
        {
            Debug.Log("Complete all missions before switching to the next day.");
        }
    }
}
