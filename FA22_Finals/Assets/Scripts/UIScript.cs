using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScript : MonoBehaviour
{
    public TMP_Text ScoreUI;
    public PlayerScript player;

    // Start is called before the first frame update
    void Start()
    {
        player.Score = 0;
        ScoreUI.text = "" + player.Score;
    }

    // Update is called once per frame
    void Update()
    {
        DisplayScore();
    }

    void DisplayScore()
    {
        ScoreUI.text = "" + player.Score;

    }
}
