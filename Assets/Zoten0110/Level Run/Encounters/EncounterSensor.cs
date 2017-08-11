using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterSensor : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {

        var encounter = collision.gameObject.GetComponentInParent<IEncounter>();
        Debug.Log(collision.gameObject);
        if (encounter)
        {
            
            encounter.StartThrowObject();
        }
    }
}
