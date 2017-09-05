using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuiExurga : IPowerup
{

    private float m_powerCellAmount;
    private float m_cellIncreaseRate;
    private float m_maxPowerCellAmount;

    [SerializeField]
    private Transform m_blazePos;
    [SerializeField]
    private GameObject m_blaze;

    [SerializeField]
    private Animator m_tool;
    [SerializeField]
    private ToolController m_contoller;

    public override Type type
    {
        get
        {
            return Type.QuiExurga;
        }
    }

    private void DischargeCell() { }
    private void GainMoney() { LevelRunMoneyHandler.Instance.GiveMoney(); }


    protected override void Death()
    {
        m_contoller.EnableInput(true);
        m_tool.SetBool("Qui Exurga", false);
    }

    //private IEnumerator VaccumStart()
    //{
    //    while(m_pointEffector.forceMagnitude != m_maxPower)
    //    {
    //        m_pointEffector.forceMagnitude -= m_vaccumIncreaseRate;
    //        if(m_pointEffector.forceMagnitude < m_maxPower)
    //        {
    //            m_pointEffector.forceMagnitude = m_maxPower;
    //        }
    //        yield return null;
    //    }
    //}

    protected override void PowerupFunction()
    {
        m_powerCellAmount += m_cellIncreaseRate;
        if (m_powerCellAmount >= m_maxPowerCellAmount)
        {
            DischargeCell();
            m_powerCellAmount = 0;
        }

        var blaze = Instantiate(m_blaze) as GameObject;
        blaze.transform.position = m_blazePos.position;

        GainMoney();
    }

    private void Start()
    {
        //if(m_maxPower > 0)
        //{
        //    m_maxPower = -m_maxPower;
        //}
        //if(m_vaccumIncreaseRate < 0)
        //{
        //    m_vaccumIncreaseRate = -m_vaccumIncreaseRate;
        //}
    }

    private void OnDisable()
    {
        //m_tool.SetBool("Qui Exurga", false);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        UpdateInfo();
        // StartCoroutine(VaccumStart());
        TimerFactory.Instance.Create("Qui Exurga", m_duration);
        m_contoller.EnableInput(false);
        StartCoroutine(DelayDestroy());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var trash = other.gameObject.GetComponentInParent<Trash>();
        if (trash)
        {
            Destroy(trash.gameObject);
            PowerupFunction();
        }
    }
}
