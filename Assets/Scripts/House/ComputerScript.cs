using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerScript : MonoBehaviour
{
    public List<GameObject> responses;

    void Awake()
    {
        StartCoroutine(PlayResponse());
    }

    IEnumerator PlayResponse()
    {
        for (int i = 0; i < responses.Count; i++)
        {
            responses[i].SetActive(true);
            yield return new WaitForSeconds(0.75f);
        }
        Destroy(gameObject);
    }
}
