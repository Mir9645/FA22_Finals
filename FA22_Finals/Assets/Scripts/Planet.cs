using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    //public GameObject player;
    Rigidbody rb;
    public GameObject player;
    public GameObject follower;
    
    Vector3 PlayerVelocity;
    Vector3 PlayerPosition;
    Vector3 FollowerPosition;

    Vector3 NewPlayerVelocity;
    Vector3 NewPlayerDirection;

    public float StartingVelocity;
    public float IncrementVelocity;

    private float rotationSpeed;
    public float RotateFactor;

    public float Timer;
    public float DurationMaxSpeed;
    public float MaxSpeed;


    public float DegreesPerSec;
    public float rotationspeed;

    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>();
       
    }

    // Update is called once per frame
    void Update()
    {
       Timer += Time.deltaTime;

        float LerpValue = Timer / DurationMaxSpeed;
        float SpeedFactor = Mathf.Lerp(1, MaxSpeed, LerpValue);

        rotationspeed = DegreesPerSec * RotateFactor * SpeedFactor* Time.deltaTime;

        Quaternion PlanetRotation = Quaternion.Euler(0, 0, rotationspeed);

        transform.rotation *= PlanetRotation;
       
        //PlayerPosition = player.GetComponent<Rigidbody2D>().position;

        //FollowerPosition = follower.GetComponent<Rigidbody>().position;
    }

  
    public void LockinOrbit(PlayerScript playercheck)
    {
        PlayerVelocity = player.GetComponent<Rigidbody2D>().velocity;
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
