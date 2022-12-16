using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameEndUI : MonoBehaviour
{
    public TMP_Text ScoreUI;
    public GameObject scoreKeeper;
   
    // Start is called before the first frame update
    void Start()
    {
        scoreKeeper = GameObject.FindObjectOfType<ScoreKeeper>().gameObject;
        ScoreUI.text = "" + scoreKeeper.GetComponent<ScoreKeeper>().Score;


    }

    // Update is called once per frame
    void Update()
    {
        DisplayScore();

    }

    void DisplayScore()
    {
        ScoreUI.text = "" + scoreKeeper.GetComponent<ScoreKeeper>().Score;

    }
}
