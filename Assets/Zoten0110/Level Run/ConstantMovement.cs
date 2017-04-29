﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMovement : MonoBehaviour {

    public Vector3 m_move;
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

    void OnEnable()
    {
        this.AddEventListenerGlobal<LevelStartEvent>(OnLevelStartEvent);
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
        this.RemoveEventListenerGlobal<LevelStartEvent>(OnLevelStartEvent);
    }
}