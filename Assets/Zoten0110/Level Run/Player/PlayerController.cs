﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Contains all the controls for the player object
/// </summary>
public class PlayerController : MonoBehaviour, DebugObject
{

    private Rigidbody2D m_rigidbody;
    [SerializeField]
    private Collider2D m_legCollider;

    [Header("Jump Properties")]
    [SerializeField]
    private float m_jumpForce;

    private bool m_isJumping;
    private bool m_canDrop;

    private bool m_enableInput;

    private bool jumpInput { get { return CustomInput.Instance.isTapped(CustomInputType.MonoInput,"Jump"); } }
    private bool dropInput { get {
            return CustomInput.Instance.isExecuted("Drop"); } }


    public void EnableInput(bool value)
    {
        m_enableInput = value;
    }

    private void OnLevelStartEvent(LevelStartEvent e)
    {
        EnableInput(true);
    }

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        this.AddEventListenerGlobal<LevelStartEvent>(OnLevelStartEvent);
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_enableInput)
            return;

        //Input Checks
        {
            if (jumpInput && !m_isJumping)
            {
                if (dropInput && m_canDrop)
                {
                    m_legCollider.isTrigger = true;
                }
                else
                {
                    m_rigidbody.AddForce(Vector3.up * m_jumpForce, ForceMode2D.Impulse);
                }
                m_isJumping = true;
            }
        }
    }

    void OnDisable()
    {
        this.RemoveEventListenerGlobal<LevelStartEvent>(OnLevelStartEvent);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        m_isJumping = false;
        m_canDrop = other.gameObject.GetComponent<PlatformEffector2D>() != null ? true : false;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        m_isJumping = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        m_legCollider.isTrigger = false;
    }

    void DebugObject.OnDebug()
    {
        m_enableInput = true;
    }
}
