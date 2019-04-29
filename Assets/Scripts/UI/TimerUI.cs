using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    public Text timerText;

    // Update is called once per frame
    void Update()
    {
        string minutes = Mathf.Floor(GameManager.timeLeft / 60).ToString("00");
        string seconds = (GameManager.timeLeft % 60).ToString("00");
        timerText.text = minutes + ":" + seconds;
    }
}
