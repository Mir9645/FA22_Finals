using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public static ScoreKeeper scoreKeeper;
    public int Score;

    // Start is called before the first frame update
    void Awake()
    {
        if (scoreKeeper == null)
        {
            scoreKeeper = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (scoreKeeper != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
