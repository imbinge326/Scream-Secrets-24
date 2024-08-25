using UnityEngine;

public class MissionObject : MonoBehaviour
{
    public string missionName; // The name of the mission this object is associated with

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Assuming the player has a tag "Player" and interacts with the object
            MissionManager missionManager = FindObjectOfType<MissionManager>();
            if (missionManager != null)
            {
                missionManager.CompleteMission(missionName);
            }

            {
                Debug.Log("Player detected");
            }

            // Optionally, you can destroy the object after completing the mission
            Destroy(gameObject);
        }
    }
}
