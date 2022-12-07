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

    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>();
       
    }

    // Update is called once per frame
    void Update()
    {
        //PlayerPosition = player.GetComponent<Rigidbody2D>().position;

        //FollowerPosition = follower.GetComponent<Rigidbody>().position;
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        PlayerScript playercheck = collider.transform.GetComponent<PlayerScript>();
        if (playercheck != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayerVelocity = player.GetComponent<Rigidbody2D>().velocity;

                playercheck.transform.SetParent(transform, true);
                playercheck.IsinOrbit = true;
            }
            //else if (Input.GetKeyUp(KeyCode.Space))
            //{
            //    playercheck.transform.SetParent(null, true);
            //    playercheck.IsinOrbit= false;

                //NewPlayerDirection = PlayerPosition - FollowerPosition;
                //NewPlayerDirection.z = 0f;

                //vector3 newplayerposition = transform.position + newplayerdirection.normalized * newplayervelocity;

                //rb.MovePosition(NewPlayerPosition);
            //}
        }
    }
}
