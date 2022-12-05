using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    Vector3 mousePosition;
    Vector3 mouseDirection;
    public float moveSpeed;
    Rigidbody2D rb;
    Vector2 position = new Vector2(0f, 0f);
    public float mousemindistance;

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
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mouseDirection = mousePosition - transform.position;
        mouseDirection.z = 0f;
        position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);

        Vector3 testMouseDirection = mousePosition - transform.position;
                testMouseDirection.z = 0f;
                if (testMouseDirection.magnitude >= mousemindistance)
        {
            mouseDirection = testMouseDirection;
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
        Vector3 targetPosition = transform.position + mouseDirection.normalized * moveSpeed;
        rb.MovePosition(targetPosition);
    }
}
