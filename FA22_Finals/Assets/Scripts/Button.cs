using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
  public void Retry ()
    {
        SceneManager.LoadScene("CometGame");

    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Application Quit");
    }
}
