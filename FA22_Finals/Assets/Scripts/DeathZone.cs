using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public float PlanetRadius;

    public LayerMask PlayerMask;

    private void Update()
    {

        Collider2D Hitcollider = Physics2D.OverlapCircle(transform.position, PlanetRadius, PlayerMask);
       
        if (Hitcollider != null)
        {
            Debug.Log(Hitcollider);

            PlayerScript TestPlayer = Hitcollider.transform.GetComponent<PlayerScript>();
            if (TestPlayer != null)
            {
                Destroy(TestPlayer.gameObject);

            }
        }
    }

}
