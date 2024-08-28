using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GiveHints : MonoBehaviour
{
    public GameObject textbox;
    public TMP_Text text;

    public void OnClick(string hint)
    {
        textbox.SetActive(true);
        text.text = hint;
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(2);
        textbox.SetActive(false);
    }
}
