using System;
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
        }
    }

    private void OnLevelStartEvent(LevelStartEvent e)
    {
        m_enableInput = true;
    }

    void Start()
    {
        m_tools[m_toolIndex].Select();
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

        SwapTool();

        if (isShooting)
        {
            Shoot?.Invoke();
        }

    }

    void OnDisable()
    {
        this.RemoveEventListenerGlobal<LevelStartEvent>(OnLevelStartEvent);
    }

    void DebugObject.OnDebug()
    {
        m_enableInput = true;
    }
}
