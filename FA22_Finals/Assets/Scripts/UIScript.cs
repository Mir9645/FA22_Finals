using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScript : MonoBehaviour
{
    public TMP_Text ScoreUI;
    public TMP_Text TimerUI;

    public float GameTime;

    public bool timerIsRunning = false;

    public ScoreKeeper scoreKeeper;

    // Start is called before the first frame update
    void Start()
    {
        scoreKeeper.Score = 0;
        ScoreUI.text = "" + scoreKeeper.Score;

        timerIsRunning = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (GameTime > 0)
            {
                GameTime -= Time.deltaTime;
                DisplayTime(GameTime);
            }
            else
            {
                Debug.Log("Time has run out!");
                GameTime = 0;
                timerIsRunning = false;
            }
        }

        DisplayScore();
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        TimerUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    void DisplayScore()
    {
        ScoreUI.text = "" + scoreKeeper.Score;

    }
}
