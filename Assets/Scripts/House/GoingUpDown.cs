using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoingUpDown : MonoBehaviour
{
    public Transform checkPoint;
    public Transform player;

    public void GoUpDown()
    {
        player.position = checkPoint.position;
    }
}
