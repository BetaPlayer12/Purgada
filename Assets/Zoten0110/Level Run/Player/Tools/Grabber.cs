using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : Tool {

    private enum State
    {
        Standby,
        Extend,
        Retract
    }

    [SerializeField]
    private Transform m_clawArm;
    [SerializeField]
    private float m_maxRange;
    [SerializeField]
    private float m_speed;
    private State m_toolState;


    public void Extend()=>
        m_toolState = State.Extend;

    public void Retract() =>
        m_toolState = State.Retract;

    public override void Activate()
    {
        if (m_lockInput)
            return;

        Debug.Log("Grabber Extend");
        Extend();
        m_lockInput = true;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        switch (m_toolState)
        {
            case State.Extend:
               
                m_clawArm.localPosition += Vector3.right * m_speed * Time.deltaTime;

                if (Vector3.Distance(Vector3.zero, m_clawArm.localPosition) > m_maxRange)
                {
                    Retract();
                }
                break;
            case State.Retract:
                m_clawArm.localPosition += Vector3.left * m_speed * Time.deltaTime;
                if(m_clawArm.localPosition.x < 0.1f)
                {
                    m_clawArm.localPosition = Vector3.zero;
                    m_toolState = State.Standby;
                    m_lockInput = false;
                }
                break;
        }
	}



}
