using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Planet[] PlanetPrefabs;
    public Asteroid[] AsteroidPrefabs;
    public int PlanetNumber;
    public int AsteroidNumber;
    public float RadiusOfSpawn;
    public float MaxAttempt;
    PlayerScript player;

    public LayerMask PlanetMask;
    public LayerMask AsteroidMask;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerScript>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Collider2D[] PlanetList = Physics2D.OverlapCircleAll(player.transform.position, RadiusOfSpawn, PlanetMask);
            for (int i = PlanetList.Length; i < PlanetNumber; i++)
            {
                if (SpawnPlanet())
                {
                    return;
                }
            }

            Collider2D[] AsteroidList = Physics2D.OverlapCircleAll(player.transform.position, RadiusOfSpawn, AsteroidMask);
            for (int i = AsteroidList.Length; i < AsteroidNumber; i++)
            {
                if (SpawnAsteroid())
                {
                    return;
                }
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

    private bool SpawnAsteroid()
    {
        int AsteroidIndex = Random.Range(0, AsteroidPrefabs.Length);
        Asteroid AsteroidPrefab = AsteroidPrefabs[AsteroidIndex];

        for (int i = 0; i < MaxAttempt; i++)
        {
            float Angle = Random.value * Mathf.PI * 2;

            Vector2 AsteroidPosition = player.transform.position;

            AsteroidPosition.x += RadiusOfSpawn * Mathf.Cos(Angle) * Random.value;
            AsteroidPosition.y += RadiusOfSpawn * Mathf.Sin(Angle) * Random.value;

            if (Physics2D.OverlapCircle(AsteroidPosition, AsteroidPrefab.AstetoridBounderies))
            {

            }
            else
            {
                Asteroid NewAsteroid = Instantiate(AsteroidPrefab);
                NewAsteroid.transform.position = AsteroidPosition;
                return true;
            }

        }

        return false;
    }
}
