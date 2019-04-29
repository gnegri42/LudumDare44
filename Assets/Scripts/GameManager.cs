using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
	public static bool gameHasEnded;
    public static int score;
    public static float timeLeft;

    public List<List<GameObject>> choosedRecipes;


    // Singleton
    void Awake()
    {
        if (gm == null)
        {
            gm = this;
            Debug.Log("GM instanciated");
        }
        gameHasEnded = false;
    }

    private void Start()
    {
        choosedRecipes = new List<List<GameObject>>();
        score = 0;
        timeLeft = 100;
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0f)
        {
            timeLeft = 0f;
            if (!gameHasEnded)
                EndGame();
        }
    }

    void EndGame()
    {
        gameHasEnded = true;
    }
} // Class