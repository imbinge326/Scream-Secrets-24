using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Keypadsystem : MonoBehaviour
{
    [SerializeField] private TMP_Text answer;
    public string correctAnswer;
    public GameObject Keypad;

    public void Number(int number)
    {
        answer.text += number.ToString();
    }

    public void OnEnter()
    {
        if (answer.ToString() == correctAnswer)
        {
            answer.text = "Correct";
        }
        else 
        {
            answer.text = "Invalid";
        }
    }

    public void OnExitButtonPressed()
    {
        Time.timeScale = 1;
    }

    public void OnKeypadCalled()
    {
        Time.timeScale = 0;
    }
}
