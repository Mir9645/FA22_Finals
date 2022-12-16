using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Build.Content;

public class AsteroidDebris : MonoBehaviour
{
    public int ScoreValue;
    public int Driftforce;
    private GameObject player;
    public GameObject scoreKeeper;
    public Rigidbody2D rb;
    public float SelfDestructTime;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        player = GameObject.FindObjectOfType<PlayerScript>().gameObject;
        scoreKeeper = GameObject.FindObjectOfType<ScoreKeeper>().gameObject;
    }

    public void Drift(Asteroid SourceAsteroid)
    {
        Vector3 asteroidDirection = transform.position - SourceAsteroid.transform.position;
        rb.AddForce(asteroidDirection.normalized * Driftforce);

        //StartCoroutine(SelfDestruct());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            scoreKeeper.GetComponent<ScoreKeeper>().Score += ScoreValue;
            Destroy(this.gameObject);
        }
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(SelfDestructTime);
        Destroy(this.gameObject);
    }
}