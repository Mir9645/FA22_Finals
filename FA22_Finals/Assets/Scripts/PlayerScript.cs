using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    Vector3 mousePosition;
    Vector3 mouseDirection;
    Vector3 NewPlayerVelocity;
    Vector3 NewPlayerDirection;

    public float moveSpeed;
    public float ControlDelay;
    public float FlingSpeed;

    public ParticleSystem sparkle;
    public AudioSource SoundEffect;
    

    public Rigidbody2D rb;
    Vector2 position = new Vector2(0f, 0f);

    public float mousemindistance;

    public bool IsinOrbit = false;
    public float TurnSpeed;
    public float _acceleration;
    public float _maxForce;
    public float PlayerRadius;
    public LayerMask PlanetMask;
    public LayerMask InnerRingMask;

    public FollowPlayer Follower;

    public Planet currentPlanet;
    public bool playerDeath;
    
    //[SerializeField]
    //public float speed;
    //[SerializeField]
    //private float rotationSpeed;

    //public float rotationSpeed;
    //public float MinSpeed;

    // Start is called before the first frame update
    void Start()
    {
        sparkle.Stop();

        playerDeath = false;
        
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector3.right);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //mousePosition = Input.mousePosition;
        //mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //mouseDirection = mousePosition - transform.position;
        //mouseDirection.z = 0f;
        //position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);

        //Vector3 testMouseDirection = mousePosition - transform.position;
        //        testMouseDirection.z = 0f;
        //        if (testMouseDirection.magnitude >= mousemindistance)
        //{
        //    mouseDirection = testMouseDirection;
        //}


        
        if (Input.GetKeyUp(KeyCode.Space) && IsinOrbit == true)
        {
            transform.SetParent(null, true);
            IsinOrbit = false;
            rb.bodyType = RigidbodyType2D.Dynamic;

            NewPlayerDirection = Follower.GetLastknownDistance();
            rb.velocity = NewPlayerDirection / Time.fixedDeltaTime * FlingSpeed; 
            moveSpeed = rb.velocity.magnitude;

            Collider2D Hitcollider = Physics2D.OverlapCircle(transform.position, PlayerRadius, PlanetMask);

            if (Hitcollider != null)
            {
                Planet TestPlanet = Hitcollider.transform.GetComponentInParent<Planet>();
                if (TestPlanet != null)
                {   
                    currentPlanet = TestPlanet;

                    TestPlanet.DegreesPerSec = 0;

                }
            }


        }

        if(Input.GetKeyDown(KeyCode.Space))
        {

            Collider2D Hitcollider = Physics2D.OverlapCircle(transform.position, PlayerRadius, PlanetMask);
          
            if (Hitcollider != null)
            {
                Planet TestPlanet = Hitcollider.transform.GetComponentInParent<Planet>();
               if (TestPlanet != null)
                {
                    TestPlanet.LockinOrbit(this);

                    currentPlanet = TestPlanet;

                }
            }

        }

            //Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //transform.position = pos;

            //Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
            
            //float horizontalInput = Input.GetAxis("Horizontal");
            //float verticalInput = Input.GetAxis("Vertical");

            //Vector2 movementDirection = new Vector2(horizontalInput, verticalInput);
            //float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
            //movementDirection.Normalize();

            //transform.Translate(movementDirection * speed * inputMagnitude * Time.deltaTime, Space.World);

            //if (movementDirection != Vector2.zero)
            //{
            //    Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movementDirection);
            //    transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            //}
        }

        private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 directioninput = new Vector3(h, v, 0).normalized;
        Vector3 currentdirection = rb.velocity.normalized;
        Vector3 finaldirection = Vector3.RotateTowards(currentdirection, directioninput, ControlDelay, 0);

        if (directioninput.magnitude <= 0.001f)
        {
            finaldirection = currentdirection;
        }

        //position = Vector2.Lerp(transform.position, directioninput, moveSpeed);






        if (IsinOrbit == false)
        {
            //Vector3 targetPosition = transform.position + mouseDirection.normalized * moveSpeed;
            //rb.MovePosition(targetPosition);



            //Vector3 Newforce = mouseDirection.normalized * moveSpeed;

            Vector3 Newforce = finaldirection.normalized * moveSpeed;
            Vector3 CurrentVelocity = rb.velocity;
            Vector3 FinalForce = Vector3.MoveTowards(CurrentVelocity, Newforce, Time.deltaTime * TurnSpeed);
            FinalForce *= Time.deltaTime;
            //rb.AddForce(Newforce);

            // Update velocities
            Vector3 targetVel = Newforce;
            Vector3 currentVel = CurrentVelocity;
            Vector3 accel = (targetVel - currentVel) / Time.fixedDeltaTime;
            if (accel.magnitude > _acceleration)
            {
                accel = accel.normalized * _acceleration;
            }


            // calculate the force needed to accelerate.
            Vector3 force = rb.mass * accel;

            if (force.magnitude > _maxForce)
            {
                force = force.normalized * _maxForce;
            }

            rb.AddForce(force);

        }

        else
        {
            Vector3 directiontoCenter = currentPlanet.transform.position - transform.position;
            transform.position += directiontoCenter.normalized * Time.fixedDeltaTime * currentPlanet.PlanetGravity;

        }
       
        Collider2D PlanetCollider = Physics2D.OverlapCircle(transform.position, PlayerRadius, PlanetMask);
        Collider2D InCircleCollider = Physics2D.OverlapCircle(transform.position, PlayerRadius, InnerRingMask);
        Debug.Log(PlanetCollider);
        Debug.Log(InCircleCollider);

        if (InCircleCollider != null)
        {

            Planet planetScript = PlanetCollider.transform.GetComponentInParent<Planet>();

            if (planetScript != null)
            {
                Vector3 directiontoCenter = planetScript.transform.position - transform.position;
                if (IsinOrbit == true)
                {
                    transform.position += directiontoCenter.normalized * Time.fixedDeltaTime * planetScript.PlanetGravity * 10;
                }
                else
                {
                    rb.AddForce(directiontoCenter.normalized * Time.fixedDeltaTime * planetScript.PlanetGravitywhenOutofOrbit * 10);
                    
                }

            }

        }
        else if (PlanetCollider != null && !Input.GetKeyDown(KeyCode.Space))
        {
           
            Planet planetScript = PlanetCollider.transform.GetComponentInParent<Planet>();

            if (planetScript != null)
            {   
                Vector3 directiontoCenter = planetScript.transform.position - transform.position;
                rb.AddForce(directiontoCenter.normalized * Time.fixedDeltaTime * planetScript.PlanetGravitywhenOutofOrbit);
            }

        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        sparkle.Play();
        SoundEffect.Play();
    }

    public void beforedeath()
    {
        SoundEffect.transform.SetParent(null);
        sparkle.transform.SetParent(null);

        SoundEffect.Play();
        sparkle.Play();

        Invoke(nameof(Death), 1.0f);
    }
    
    public void Death()
    {
        playerDeath = true;
       
    }

}
