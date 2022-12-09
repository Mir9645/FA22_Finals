using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public int followDistance;
    private List<Vector3> storedPositions;

    void Awake()
    {
        storedPositions = new List<Vector3>(); 

    }

    void Start()
    {

    }

    public Vector3 GetLastknownDistance()
    {
        return storedPositions[storedPositions.Count - 1] - storedPositions[storedPositions.Count - 2];
    }

    void FixedUpdate()
    {
        if (storedPositions.Count == 0)
        {
            Debug.Log("blank list");
            storedPositions.Add(player.transform.position); 
            return;
        }
        else if (storedPositions[storedPositions.Count - 1] != player.transform.position)
        {
            storedPositions.Add(player.transform.position); 
        }

        if (storedPositions.Count > followDistance)
        {
            transform.position = storedPositions[0];
            storedPositions.RemoveAt(0);
        }
    }
}
