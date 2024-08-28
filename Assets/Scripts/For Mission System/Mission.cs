using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Mission
{
    public string missionName;        // Name of the mission
    public string description;        // Description of the mission
    public List<string> objectives;   // List of objectives for the mission
    public UnityEvent onMissionComplete; // Event triggered when the mission is completed
    public bool isCompleted = false;  // Track if the mission is completed
}


