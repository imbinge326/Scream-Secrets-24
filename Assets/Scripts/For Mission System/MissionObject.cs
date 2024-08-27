using UnityEngine;

public class MissionObject : MonoBehaviour
{
    public string missionName; // The name of the mission this object is associated with
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    void Start()
    {
        // Store the original position and rotation of the object
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

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

                    // Respawn the object
                    RespawnObject();
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

    void RespawnObject()
    {
        // Instantiate a new copy of this object at the original position and rotation
        GameObject newObject = Instantiate(gameObject, originalPosition, originalRotation);

        // Optionally, reset certain properties of the new object
        MissionObject newMissionObject = newObject.GetComponent<MissionObject>();
        newMissionObject.missionName = missionName; // Ensure the new object has the correct mission name

        // Destroy the old object (optional)
        Destroy(gameObject);

    }

}