using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Test : MonoBehaviour
{
    [Header("Phone Settings")]
    public GameObject phoneMenu;
    public GameObject notif1;

    [Header("Messages")]
    public GameObject[] responses;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && phoneMenu.activeSelf == false)
        {
            phoneMenu.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.F) && phoneMenu.activeSelf == true)
        {
            phoneMenu.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.X) && notif1.activeSelf == false)
        {
            notif1.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.X) && notif1.activeSelf == true)
        {
            notif1.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Y) && responses[0].activeSelf == false)
        {
            StartCoroutine(PlayResponse());
        }
        else if (Input.GetKeyDown(KeyCode.Y) && responses[0].activeSelf == true)
        {
            foreach(GameObject response in responses)
            {
                response.SetActive(false);
            }
        }
    }

    IEnumerator PlayResponse()
    {
        for (int i = 0; i < responses.Length; i++)
        {
            responses[i].SetActive(true);

            yield return new WaitForSeconds(1.5f);
        }
    }
}
