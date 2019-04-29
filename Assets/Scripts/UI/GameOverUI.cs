using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public RectTransform gamePanel;
    public Text scoreText;
    public GameObject endGameUI;

    private void Start()
    {
        scoreText.text = "Time's Over ! Your score is : " + GameManager.score.ToString();
    }

    private void Update()
    {
        if (GameManager.gameHasEnded)
            endGameUI.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //gameObject.SetActive(false);
    }
}
