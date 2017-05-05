﻿using System;
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
    private Grabber_ClawArm m_clawArm;
    [SerializeField]
    private float m_maxRange;
    [SerializeField]
    private float m_speed;
    private State m_toolState;

    private bool m_isJammed;
    [SerializeField]
    private float m_jammedTime;

    private Transform m_clawArmTransform;

    private void Dispose()
    {
        if (m_clawArm.isEmptyHanded)
            return;

        if (m_clawArm.heldTrash.trashType == Trash.Type.Toxic)
        {
            OnSuccesfulDisposal();
        }
        else
        {
            OnFailedDisposal();
        }

        Destroy(m_clawArm.heldTrash.gameObject);
    }

    private IEnumerator JamCountDown()
    {
        m_isJammed = true;
        OnJamStart();
        yield return new WaitForSeconds(m_jammedTime);
        OnJamEnd();
        m_isJammed = false;
    }

    private void OnJamStart()
    {
        Debug.Log("Grabber Jams");
    }

    private void OnJamEnd()
    {
        Debug.Log("Grabber Operational");
    }

    protected void OnSuccesfulDisposal()
    {
        Debug.Log("Grabber Disposed Trash");
    }

    protected void OnFailedDisposal()
    {
        StartCoroutine(JamCountDown());
    }

    protected override void OnSelect()
    {
        m_clawArm.Enable(true);
    }

    public override void Unselect()
    {
        m_clawArm.Enable(false);
    }

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

    void Start()
    {
        m_clawArmTransform = m_clawArm.transform;
        m_toolState = State.Standby;
    }

	// Update is called once per frame
	void FixedUpdate ()
    {
        switch (m_toolState)
        {
            case State.Extend:

                m_clawArmTransform.localPosition += Vector3.right * m_speed * Time.deltaTime;

                if (Vector3.Distance(Vector3.zero, m_clawArmTransform.localPosition) > m_maxRange)
                {
                    Retract();
                }
                break;
            case State.Retract:
                m_clawArmTransform.localPosition += Vector3.left * m_speed * Time.deltaTime;
                if(m_clawArmTransform.localPosition.x < 0.1f)
                {
                    m_clawArmTransform.localPosition = Vector3.zero;
                    m_toolState = State.Standby;
                    m_lockInput = false;
                    Dispose();
                }
                break;
        }
	}


}
