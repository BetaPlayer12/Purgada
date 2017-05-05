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
    private Collider2D m_collider;

    private bool m_activated;

    protected void OnSuccesfulDisposal()
    {
        Debug.Log("Recyclaton Disposed Trash");
    }

    protected void OnFailedDisposal()
    {
        Debug.Log("Recyclaton Fails");
    }

    protected override void OnSelect()
    {
        m_collider.enabled = true;
    }

    public override void Unselect()
    {
        m_collider.enabled = false;
    }

    public override void Activate()
    {
        m_activated = true;
    }
	
    void Start()
    {
        m_range.forceMagnitude =  0;
        m_collider = GetComponent<Collider2D>();
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

    void OnTriggerEnter2D(Collider2D other)
    {
        var trash = other.gameObject.GetComponentInParent<Trash>();
        if (trash)
        {
            if(trash.trashType == Trash.Type.Recyclable)
            {
                OnSuccesfulDisposal();
            }
            else
            {
                OnFailedDisposal();
            }
            Destroy(trash.gameObject);
        }
    }
}
