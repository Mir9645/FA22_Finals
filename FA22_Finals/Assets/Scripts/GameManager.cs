using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UIScript UIscript;
    public PlayerScript player;


    // Start is called before the first frame update

    private void Awake()
    {
      
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (UIscript.timerIsRunning == false && player.playerDeath == false)
        {
            Debug.Log("Game Clear");
            SceneManager.LoadScene("GameClear");
        }
        else if (player.playerDeath == true)
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene("GameOver");

        }

    }
}
