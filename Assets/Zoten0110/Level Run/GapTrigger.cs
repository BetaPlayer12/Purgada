using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GapTrigger : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponentInParent<PlayerHealth>())
        {
            Debug.Log("Called it");
            this.RaiseEventGlobal<PlayerDeathEvent>(new PlayerDeathEvent(gameObject));
        }
    }
}
