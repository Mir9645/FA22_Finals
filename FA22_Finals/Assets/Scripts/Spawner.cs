using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Planet[] PlanetPrefabs;
    public GameObject[] AsteroidPrefabs;
    public int PlanetNumber;
    public int AsteroidNumber;
    public float RadiusOfSpawn;
    public float MaxAttempt;
    PlayerScript player;

    public LayerMask PlanetMask;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerScript>();  
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] PlanetList = Physics2D.OverlapCircleAll(player.transform.position, RadiusOfSpawn, PlanetMask);
        for (int i = PlanetList.Length; i < PlanetNumber; i++)
        {
            if (SpawnPlanet())
            {
                return;
            }
        }
    }

    private bool SpawnPlanet()
    {
        int PlanetIndex = Random.Range(0, PlanetPrefabs.Length);
            Planet PlanetPrefab = PlanetPrefabs[PlanetIndex];

        for (int i = 0; i < MaxAttempt; i++)
        {
            float Angle = Random.value * Mathf.PI * 2;

            Vector2 PlanetPosition = player.transform.position;

            PlanetPosition.x += RadiusOfSpawn * Mathf.Cos(Angle) * Random.value;
            PlanetPosition.y += RadiusOfSpawn * Mathf.Sin(Angle) * Random.value;

            if (Physics2D.OverlapCircle(PlanetPosition, PlanetPrefab.PlanetRadius))
            {

            }
            else
            {
                Planet NewPlanet = Instantiate(PlanetPrefab);
                NewPlanet.transform.position = PlanetPosition;
                return true;
            }

        }

        return false;

    }
}
