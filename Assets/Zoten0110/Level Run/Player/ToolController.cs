﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains Behavioour for Tools
/// </summary>
public class ToolController : MonoBehaviour, DebugObject {

    public delegate bool AllClearFunc();
    public delegate void ShootFunc();
    public AllClearFunc AllClear;
    public ShootFunc Shoot;


    [SerializeField]
    private Tool[] m_tools;
    private int m_toolIndex;

    [SerializeField]
    private Animator m_animator;

    private bool m_enableInput;

    private bool isShooting { get { return CustomInput.Instance.isHeld(CustomInputType.MonoInput,"Shoot"); } }
    private bool isSwapingTool { get { return CustomInput.Instance.isTapped(CustomInputType.BinaryInput, "Swap Tools"); } }

    private void SwapTool()
    {
        if (AllClear() && isSwapingTool)
        {
            m_toolIndex += CustomInput.Instance.GetTapValue(CustomInputType.BinaryInput,"Swap Tools");
            m_toolIndex = m_toolIndex.RotateIndex(0, m_tools.Length);
            m_tools[m_toolIndex].Select();
            m_animator.SetInteger("Tool Index", m_toolIndex);
        }
    }

    private void OnLevelStartEvent(LevelStartEvent e)
    {
        EnableInput(true);
    }

    public void EnableInput(bool value)
    {
        m_enableInput = value;
    }

    void Start()
    {
        m_tools[m_toolIndex].Select();
    }

    void OnEnable()
    {
        this.AddGameEventListenerGlobal<LevelStartEvent>(OnLevelStartEvent);
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_enableInput)
            return;

        SwapTool();

        if (isShooting)
        {
            Shoot?.Invoke();
        }

    }

    void OnDisable()
    {
        this.RemoveGameEventListenerGlobal<LevelStartEvent>(OnLevelStartEvent);
    }

    void DebugObject.OnDebug()
    {
        m_enableInput = true;
    }
}
