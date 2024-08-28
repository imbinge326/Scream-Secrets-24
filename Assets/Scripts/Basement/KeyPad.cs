using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KeyPad : MonoBehaviour
{
    [SerializeField] private TMP_Text answer;
    public string home;

    public void Number(int number)
    {
        answer.text += number.ToString();
    }

    public void OnEnter()
    {
        if (answer.text == "9696")
        {
            answer.text = "CORRECT";
            SceneManager.LoadScene(home);
        }
        else 
        {
            answer.text = "INVALID";
        }
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(1);
        answer.text = "";
    }
}
