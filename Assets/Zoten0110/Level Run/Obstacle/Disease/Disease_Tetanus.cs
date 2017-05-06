using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disease_Tetanus : IDisease
{
    [SerializeField]
    private PlayerController m_playerController;
    [SerializeField]
    private ToolController m_toolController;

    protected override void DiseaseEnd()
    {
        m_playerController.EnableInput(true);
        m_toolController.EnableInput(true);
    }

    protected override void DiseaseStart()
    {
        TimerFactory.Instance.Create("Tetanus", m_duration);
        m_playerController.EnableInput(false);
        m_toolController.EnableInput(false);
    }
}
