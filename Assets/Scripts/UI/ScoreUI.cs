using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreUI : MonoBehaviour
{
    [Header("UI")]
    // UI Elements
    public Text scoreText;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Soul Pieces : " + GameManager.score.ToString();
    }
}
