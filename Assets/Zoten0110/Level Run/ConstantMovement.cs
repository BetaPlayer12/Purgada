﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMovement : MonoBehaviour {

    public Vector3 m_move;
    [SerializeField]
    private bool m_manualOverride;
    [SerializeField]
    private bool m_active;
	
    public void Move()
    {
        m_active = true;
    }

    private void OnLevelStartEvent(LevelStartEvent e)
    {
        Move();
    }

    private void Start()
    {
        if (!m_manualOverride)
        {
            m_move = Vector3.left * GlobalGameSettings.Instance.platformSpeed;
        }
    }

    void OnEnable()
    {
        this.AddGameEventListenerGlobal<LevelStartEvent>(OnLevelStartEvent);
    }

	// Update is called once per frame
	void Update () {
        if (m_active)
        {
            transform.position += m_move * Time.deltaTime;
        }
	}

    void OnDisable()
    {
        this.RemoveGameEventListenerGlobal<LevelStartEvent>(OnLevelStartEvent);
    }
}
