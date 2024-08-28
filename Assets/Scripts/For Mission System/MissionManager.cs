using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour
{
    private List<Day> days = new List<Day>();
    public int currentDayIndex = 0; // Default to 0, but this will be set by PlayerPrefs
    private Day currentDay;

    public Text currentMissionsText;
    public Button nextDayButton;

    void Start()
    {
        // Read day index from PlayerPrefs
        currentDayIndex = PlayerPrefs.GetInt("DayIndex", 0);

        // Initialize missions here or call GameState to load the data
        if (days.Count == 0)
        {
            Debug.LogWarning("Days list is empty. Ensure days are set by MissionManagerSecond.");
        }

        // Start the day based on the index set by MissionManagerSecond
        if (days.Count > 0)
        {
            StartDay(currentDayIndex);
        }

        if (nextDayButton != null)
        {
            nextDayButton.onClick.AddListener(TriggerNextDay);
            nextDayButton.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (nextDayButton != null && CanSwitchDay())
        {
            nextDayButton.gameObject.SetActive(true);
        }
        else if (nextDayButton != null)
        {
            nextDayButton.gameObject.SetActive(false);
        }
    }

    // Add GetDays method
    public List<Day> GetDays()
    {
        return new List<Day>(days); // Return a copy of the list
    }

    // Add SetDays method
    public void SetDays(List<Day> newDays)
    {
        days = new List<Day>(newDays); // Set the days list
        // Optionally start the first day or handle other initialization here
        if (days.Count > 0)
        {
            StartDay(currentDayIndex); // Start the day based on current index
        }
    }

    void StartDay(int dayIndex)
    {
        if (dayIndex < days.Count)
        {
            currentDay = days[dayIndex];
            Debug.Log($"Starting day: {currentDay.dayName}");
            ActivateMissions(currentDay);
            UpdateMissionUI();
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
        Mission mission = currentDay.missions.Find(m => m.missionName == missionName);
        if (mission != null && !mission.isCompleted)
        {
            mission.isCompleted = true;
            Debug.Log($"Mission '{mission.missionName}' completed.");

            if (CanSwitchDay())
            {
                Debug.Log($"All missions for {currentDay.dayName} completed.");
            }
        }

        UpdateMissionUI();
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

    public void SwitchToNextDay()
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
}
