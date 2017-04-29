using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains Behavioour for Tools
/// </summary>
public class ToolController : MonoBehaviour {

    public delegate void ShootFunc();
    public ShootFunc Shoot;

    [SerializeField]
    private Tool[] m_tools;
    private int m_toolIndex;

    private bool m_enableInput;

    private bool isShooting { get { return Input.GetAxis("Shoot") > 0; } }
    private bool isSwapingTool { get { return Input.GetAxis("Swap Tool") != 0; } }

    private void SwapTool()
    {

        if (isSwapingTool)
        {
            m_toolIndex += (int)Input.GetAxis("Swap Tool");
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
        Shoot = m_tools[m_toolIndex].Shoot;
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

}
