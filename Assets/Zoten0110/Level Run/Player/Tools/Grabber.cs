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
    private Grabber_ClawArm m_clawArm;
    [SerializeField]
    private float m_maxRange;
    [SerializeField]
    private float m_extendSpeed;
    [SerializeField]
    private float m_retractSpeed;
    private State m_toolState;

    private bool m_isJammed;
    [SerializeField]
    private float m_jammedTime;
    [SerializeField]
    private bool m_canSwitch;

    [Header("Visual Settings")]
    [SerializeField]
    private LineRenderer m_lineRenderer;
    [SerializeField]
    private ParticleSystem m_jamFX;

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

    private IEnumerator LockInputTimer()
    {
        m_lockInput = true;
        yield return new WaitForSeconds(0.5f);
        m_lockInput = false;
    }

    private void OnJamStart()
    {
        Debug.Log("Grabber Jams");
        m_jamFX.gameObject.SetActive(true);
    }

    private void OnJamEnd()
    {
        Debug.Log("Grabber Operational");
        m_jamFX.gameObject.SetActive(false);
    }

    protected override bool AllClear()
    {
        return base.AllClear() && m_canSwitch;
    }

    protected void OnSuccesfulDisposal()
    {
        Debug.Log("Grabber Disposed Trash");
    }

    protected void OnFailedDisposal()
    {
        StopAllCoroutines();
        TimerFactory.Instance.Create("Grabber Jam", m_jammedTime);
        StartCoroutine(JamCountDown());
    }

    public void Extend()
    {
        m_canSwitch = false;
        m_toolState = State.Extend;
        m_clawArm.Enable(true);
    }

    public void Retract()
    {
        m_toolState = State.Retract;
        m_clawArm.Enable(false);
    }

     

    public override void Activate()
    {
        if (m_lockInput || m_isJammed)
            return;

        Debug.Log("Grabber Extend");

        if (m_toolState == State.Standby)
        {
            Extend();
            StopAllCoroutines();
            StartCoroutine(LockInputTimer());
        }
        else if(m_toolState == State.Extend)
        {
            Retract();
            m_lockInput = true;
        }
    }

    void Start()
    {
        m_clawArmTransform = m_clawArm.transform;
        m_clawArm.Enable(false);
        m_toolState = State.Standby;
        m_lineRenderer.positionCount = 2;
        m_canSwitch = true;
    }

	// Update is called once per frame
	void FixedUpdate ()
    {
        switch (m_toolState)
        {
            case State.Extend:

                m_clawArmTransform.localPosition += Vector3.right * m_extendSpeed * Time.deltaTime;

                if (Vector3.Distance(Vector3.zero, m_clawArmTransform.localPosition) > m_maxRange)
                {
                    Retract();
                    m_lockInput = true;
                }
                break;
            case State.Retract:
                m_clawArmTransform.localPosition += Vector3.left * m_retractSpeed * Time.deltaTime;
                if(m_clawArmTransform.localPosition.x < 0.1f)
                {
                    m_clawArmTransform.localPosition = Vector3.zero;
                    m_toolState = State.Standby;
                    m_lockInput = false;
                    m_canSwitch = true;
                    Dispose();
                }
                break;
        }

        m_lineRenderer.SetPosition(0, transform.position);
        m_lineRenderer.SetPosition(1, m_clawArmTransform.position);

    }


}
