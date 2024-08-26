using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject phoneMenu;
    public GameObject notif1;

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
    }
}
