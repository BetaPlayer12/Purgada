using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IPickup : MonoBehaviour {

    protected abstract void OnPickup(GameObject GO);

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            OnPickup(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            OnPickup(collision.gameObject);
            Destroy(gameObject);
        }
    }



}
