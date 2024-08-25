using UnityEngine;

public class MissionObject : MonoBehaviour
{
    public string missionName; // The name of the mission this object is associated with

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Find the MissionManager in the scene
            MissionManager missionManager = FindObjectOfType<MissionManager>();
            if (missionManager != null)
            {
                // Check if the mission is part of the current day's missions
                if (missionManager.IsMissionActive(missionName))
                {
                    // Complete the mission
                    missionManager.CompleteMission(missionName);
                    Debug.Log($"Mission '{missionName}' completed by player.");

                    // Optionally, destroy the object after completing the mission
                    //Destroy(gameObject);
                }
                else
                {
                    Debug.LogWarning($"Mission '{missionName}' is not available on the following day");
                }
            }
            else
            {
                Debug.LogWarning("MissionManager not found in the scene.");
            }
        }
    }

}