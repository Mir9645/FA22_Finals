using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
   
    private GameObject player;

    Vector3 PlayerVelocity;

    public float PlanetRadius;

    public float RotationSpeed;

    public float Timer;
    public float DurationMaxSpeed;
    public float MaxSpeed;
    

    public float DegreesPerSec;

    public float PlanetGravity;
    public float PlanetGravitywhenOutofOrbit;

    // Start is called before the first frame update
    void Start()
    {
       player = GameObject.FindObjectOfType<PlayerScript>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
       Timer += Time.deltaTime;

        float LerpValue = Timer / DurationMaxSpeed;
        float SpeedFactor = Mathf.Lerp(1, MaxSpeed, LerpValue);
        
        float rotationspeed = DegreesPerSec * RotationSpeed * SpeedFactor * Time.deltaTime;

        Quaternion PlanetRotation = Quaternion.Euler(0, 0, rotationspeed);

        transform.rotation *= PlanetRotation;
       
        //PlayerPosition = player.GetComponent<Rigidbody2D>().position;

        //FollowerPosition = follower.GetComponent<Rigidbody>().position;
    }

    //public void ResetRotation(Planet gameObject)
    //{
    //    gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    //}

    public void LockinOrbit(PlayerScript playercheck)
    {


        PlayerVelocity = player.GetComponent<Rigidbody2D>().velocity;
        Debug.Log(PlayerVelocity);
        Vector3 distancefromCenter = playercheck.transform.position - transform.position;
        float Radius = distancefromCenter.magnitude;
        DegreesPerSec = ((PlayerVelocity.magnitude) / (2 * Mathf.PI * Radius)) * 360;

        playercheck.transform.SetParent(transform, true);
        playercheck.IsinOrbit = true;
        playercheck.rb.bodyType = RigidbodyType2D.Kinematic;
        playercheck.rb.velocity = Vector3.zero;

        Timer = 0;
    }
}
