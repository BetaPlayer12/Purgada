using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    [SerializeField]
    private DiseaseType m_inflictDisease;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.RaiseEventGlobal(new InflictDiseaseToPlayerEvent(gameObject, m_inflictDisease));
        }
    }
}
