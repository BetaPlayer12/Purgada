using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoilCanon_Projectile : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("SoilCanon Hit");
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("SoilCanon Hit");
        Destroy(gameObject);
    }
}
