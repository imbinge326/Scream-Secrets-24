using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteraction : MonoBehaviour
{
    public bool IsInRange;
    public UnityEvent InteractAction;
    private KeyCode InteractKey = KeyCode.F;
    

    void Update()
    {
        if (IsInRange)
        {
            if (Input.GetKeyDown(InteractKey))
            {
                InteractAction.Invoke();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IsInRange = true;
            //Debug.Log("Player in range");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IsInRange = false;
            //Debug.Log("Player exit range");
        }
    }
}
