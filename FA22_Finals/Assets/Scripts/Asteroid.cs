using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public GameObject[] asteroidDebris;
    public Transform[] Transforms;
    public Sprite Original;
    public Sprite Damaged;

    public GameObject scoreKeeper;
    private int ScoreValue;

    public float AstetoridBounderies;
    public float SelfDestructTime;

    public bool Destroyed = false;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        scoreKeeper = GameObject.FindObjectOfType<ScoreKeeper>().gameObject;

        spriteRenderer.sprite = Original;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {

        if (Destroyed == false)
        {
            PlayerScript player = col.transform.GetComponent<PlayerScript>();
            if (player != null)
            {
                float ImpactSpeed = player.rb.velocity.magnitude;
                Debug.Log(ImpactSpeed);

                ScoreValue = Mathf.RoundToInt(ImpactSpeed) * 10;

                scoreKeeper.GetComponent<ScoreKeeper>().Score += ScoreValue;
            }

            Debug.Log("Collision");
            breakApart();
            spriteRenderer.sprite = Damaged;
            GetComponent<Collider2D>().isTrigger= true;
            
            Destroyed = true;

            StartCoroutine(SelfDestruct());
        }
    }

    private void breakApart()
    {
        int TransformCount = 0;

        foreach(GameObject i in asteroidDebris)
        {
            Transform dustposition = Transforms[TransformCount];
            
            GameObject newdust = Instantiate(i, dustposition.position, transform.rotation);
            AsteroidDebris dustScript = newdust.GetComponent<AsteroidDebris>();

            dustScript.Drift(this);
            
            TransformCount++;
        }
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(SelfDestructTime);
        Destroy(gameObject);
    }
}
