using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public GameObject[] asteroids;
    public Transform[] Transforms;
    public Sprite Original;
    public Sprite Damaged;

    public bool Destroyed = false;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = Original;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Collided");
        if (Destroyed == false)
        {
            breakApart();
            spriteRenderer.sprite = Damaged;
            
            Destroyed = true;
        }
    }

    private void breakApart()
    {
        int TransformCount = 0;

        foreach(GameObject i in asteroids)
        {
            Transform dustposition = Transforms[TransformCount];
            Instantiate(i, dustposition.position, transform.rotation);
            TransformCount++;
        }
    }
}
