using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameEndUI : MonoBehaviour
{
    public TMP_Text ScoreUI;
    public ScoreKeeper scoreKeeper;
   
    // Start is called before the first frame update
    void Start()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        ScoreUI.text = "" + scoreKeeper.Score;

        Destroy(scoreKeeper.gameObject);
    }

    // Update is called once per frame
  
   
}
