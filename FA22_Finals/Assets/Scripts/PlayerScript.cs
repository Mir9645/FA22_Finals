using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    Vector3 mousePosition;
    Vector3 mouseDirection;

    public float moveSpeed;
    public Rigidbody2D rb;
    Vector2 position = new Vector2(0f, 0f);

    public float mousemindistance;

    public bool IsinOrbit = false;
    public float TurnSpeed;
    public float _acceleration;
    public float _maxForce;
    public float PlayerRadius;
    public LayerMask PlanetMask;

    Planet planetScript;

    //[SerializeField]
    //public float speed;
    //[SerializeField]
    //private float rotationSpeed;

    //public float rotationSpeed;
    //public float MinSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        rb = GetComponent<Rigidbody2D>();
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
            
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {

            Collider2D Hitcollider = Physics2D.OverlapCircle(transform.position, PlayerRadius, PlanetMask);
            Debug.Log(Hitcollider);
            if (Hitcollider != null)
            {
                Planet TestPlanet = Hitcollider.transform.GetComponent<Planet>();
               if (TestPlanet != null)
                {
                    TestPlanet.LockinOrbit(this);
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

        Vector3 directioninput = new Vector3(h, v, 0);


        if (IsinOrbit == false)
        {
            //Vector3 targetPosition = transform.position + mouseDirection.normalized * moveSpeed;
            //rb.MovePosition(targetPosition);



            //Vector3 Newforce = mouseDirection.normalized * moveSpeed;

            Vector3 Newforce = directioninput.normalized * moveSpeed;
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
            // drag down in to orbit
        }
       
    }
}
