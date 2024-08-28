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
        // Day 1
        Day day1 = new Day { dayName = "Day 1" };
        Mission walkMission1 = new Mission
        {
            missionName = "Walk Few Steps",
            description = "Walk a few steps to complete this mission.",
            objectives = new List<string> { "Walk a few steps" }
        };
        day1.missions = new List<Mission> { walkMission1 };
        days.Add(day1);

        // Day 2
        Day day2 = new Day { dayName = "Day 2" };
        Mission walkMission2 = new Mission
        {
            missionName = "Walk Few Steps",
            description = "Walk a few steps to complete this mission.",
            objectives = new List<string> { "Walk a few steps" }
        };
        Mission lookAroundMission = new Mission
        {
            missionName = "Look Around",
            description = "Look around to observe your surroundings.",
            objectives = new List<string> { "Look around" }
        };
        day2.missions = new List<Mission> { walkMission2, lookAroundMission };
        days.Add(day2);

        // Day 3
        Day day3 = new Day { dayName = "Day 3" };
        Mission walkMission3 = new Mission
        {
            missionName = "Walk Few Steps",
            description = "Walk a few steps to complete this mission.",
            objectives = new List<string> { "Walk a few steps" }
        };
        Mission lookAroundMission3 = new Mission
        {
            missionName = "Look Around",
            description = "Look around to observe your surroundings.",
            objectives = new List<string> { "Look around" }
        };
        Mission touchSphereMission = new Mission
        {
            missionName = "Touch a Sphere",
            description = "Find and touch the sphere to complete the mission.",
            objectives = new List<string> { "Touch a sphere" }
        };
        day3.missions = new List<Mission> { walkMission3, lookAroundMission3, touchSphereMission };
        days.Add(day3);
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
                // Display mission objectives, start tracking progress, etc.
            }
        }
    }

    public void CompleteMission(string missionName)
    {
        Mission mission = currentDay.missions.Find(m => m.missionName == missionName);
        if (mission != null && !mission.isCompleted)
        {
            mission.isCompleted = true;
            mission.onMissionComplete.Invoke();
            Debug.Log($"Mission '{mission.missionName}' completed.");
        }
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
}
