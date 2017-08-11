using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDeath : MonoBehaviour {

    [SerializeField]
    private float m_duration;

	// Update is called once per frame
	void Update () {
        m_duration -= Time.deltaTime;
        if(m_duration <= 0)
        {
            Destroy(gameObject);
        }

    }
}
