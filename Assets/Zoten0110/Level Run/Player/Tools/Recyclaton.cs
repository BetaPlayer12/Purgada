using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recyclaton : Tool {

    [SerializeField]
    private PointEffector2D m_range;
    [SerializeField]
    private float m_maxForce;
    [SerializeField]
    private float m_speed;

    private bool m_activated;

    public override void Activate()
    {
        m_activated = true;
    }
	
    void Start()
    {
        m_range.forceMagnitude =  0;
    }

	// Update is called once per frame
	void Update () {

        if (m_activated)
        {
            m_range.forceMagnitude -= m_speed * Time.deltaTime;

            if(m_range.forceMagnitude < -m_maxForce)
            {
                m_range.forceMagnitude = -m_maxForce;
            }
        }
        else
        {
            m_range.forceMagnitude +=  m_speed * Time.deltaTime;

            if (m_range.forceMagnitude > 0)
            {
                m_range.forceMagnitude = 0;
            }
        }
        m_activated = false;

    }
}
