using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [Header("Phone Settings")]
    public GameObject phoneMenu;
    public GameObject notif;

    [Header("Messages")]
    public GameObject[] notifications;
    [SerializeField] private List<GameObject> responses;

    private void Awake()
    {
        SetResponse(0);
    }

    void Update()
    {
        // Toggle Phone UI (Frame only)
        if (Input.GetKeyDown(KeyCode.F) && !phoneMenu.activeSelf)
        {
            phoneMenu.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.F) && phoneMenu.activeSelf)
        {
            phoneMenu.SetActive(false);
        }

        // Toggle current notification initial content
        if (Input.GetKeyDown(KeyCode.X) && !notif.activeSelf)
        {
            notif.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.X) && notif.activeSelf)
        {
            notif.SetActive(false);
        }

        // Display responses set for current notification [Developer Tool]
        if (Input.GetKeyDown(KeyCode.Y) && responses.Count > 0 && !responses[0].activeSelf)
        {
            StartCoroutine(PlayResponse());
        }
        else if (Input.GetKeyDown(KeyCode.Y) && responses.Count > 0 && responses[0].activeSelf)
        {
            foreach (GameObject response in responses)
            {
                response.SetActive(false);
            }
        }

        // Initialize responses for 1st notification (0 in notifications array) [Developer Tool]
        if (Input.GetKeyDown(KeyCode.J))
        {
            SetResponse(0);
        }

        // Initialize responses for 2nd notification (1 in notifications array) [Developer Tool]
        if (Input.GetKeyDown(KeyCode.K))
        {
            SetResponse(1);
        }
    }

    // Call to display player responses
    IEnumerator PlayResponse()
    {
        for (int i = 0; i < responses.Count; i++)
        {
            responses[i].SetActive(true);
            yield return new WaitForSeconds(1.5f);
        }
    }

    // Call and pass notification number (in array, set in editor)
    public void SetResponse(int notificationNumber)
    {
        // Reset List to 0
        responses.Clear();

        notif = notifications[notificationNumber];

        foreach (Transform child in notif.transform)
        {
            if (child.CompareTag("Response"))
            {
                responses.Add(child.gameObject);
            }
        }
    }
}
