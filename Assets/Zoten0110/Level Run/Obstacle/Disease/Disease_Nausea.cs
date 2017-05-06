using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disease_Nausea : IDisease
{

    [SerializeField]
    private Cursor_Jitter m_jitter;
    [SerializeField]
    private Vector2 m_jitterRange;
    [SerializeField]
    private float m_speed;

    protected override void DiseaseEnd()
    {
        Debug.Log("Jitter End");
        m_jitter.Jitter(false);
    }

    protected override void DiseaseStart()
    {
        m_jitter.Jitter(true);
    }

    void Start()
    {
        m_jitter.SetJitter(m_jitterRange, m_speed);
    }
}
