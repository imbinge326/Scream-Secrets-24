using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class MissionObject2 : MonoBehaviour
{
    public string missionName; // The name of the mission this object is associated with
    public bool IsInRange;
    public GameObject interactTextBox;
    public UnityEvent InteractAction;
    private KeyCode InteractKey = KeyCode.F;


    void Update()
    {
        if (IsInRange)
        {
            if (Input.GetKeyDown(InteractKey))
            {
                // Assuming the player has a tag "Player" and interacts with the object
                MissionManagerSecond missionManager = FindObjectOfType<MissionManagerSecond>();
                if (missionManager != null)
                {
                    missionManager.CompleteMission(missionName);
                }

                {
                    Debug.Log("Player detected");
                }

            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsInRange = true;
            interactTextBox.SetActive(true);


        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IsInRange = false;
            interactTextBox.SetActive(false);
            //Debug.Log("Player exit range");
        }
    }
}