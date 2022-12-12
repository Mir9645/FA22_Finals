using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    //public GameObject player;
    Rigidbody rb;
    private GameObject player;
    private GameObject follower;
    
    Vector3 PlayerVelocity;

    public float PlanetRadius;

    public float RotationSpeed;

    public float Timer;
    public float DurationMaxSpeed;
    public float MaxSpeed;
    

    public float DegreesPerSec;

    public float PlanetGravity;
    public float PlanetInnerRingForce;

    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>();
       player = GameObject.FindObjectOfType<PlayerScript>().gameObject;
       follower = GameObject.FindObjectOfType<FollowPlayer>().gameObject;
       
    }

    // Update is called once per frame
    void Update()
    {
       Timer += Time.deltaTime;

        float LerpValue = Timer / DurationMaxSpeed;
        float SpeedFactor = Mathf.Lerp(1, MaxSpeed, LerpValue);
        
        float rotationspeed = DegreesPerSec * RotationSpeed * SpeedFactor* Time.deltaTime;

        Quaternion PlanetRotation = Quaternion.Euler(0, 0, rotationspeed);

        transform.rotation *= PlanetRotation;
       
        //PlayerPosition = player.GetComponent<Rigidbody2D>().position;

        //FollowerPosition = follower.GetComponent<Rigidbody>().position;
    }

  
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
