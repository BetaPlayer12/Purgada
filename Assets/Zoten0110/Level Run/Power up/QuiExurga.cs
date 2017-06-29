using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuiExurga : IPowerup {

    private float m_powerCellAmount;
    private float m_cellIncreaseRate;
    private float m_maxPowerCellAmount;

    [SerializeField]
    private PointEffector2D m_pointEffector;
    private float m_maxPower;
    private float m_vaccumIncreaseRate;

    private void DischargeCell() { }
    private void GainMoney() { }

    private IEnumerator VaccumStart()
    {
        while(m_pointEffector.forceMagnitude != m_maxPower)
        {
            m_pointEffector.forceMagnitude -= m_vaccumIncreaseRate;
            if(m_pointEffector.forceMagnitude < m_maxPower)
            {
                m_pointEffector.forceMagnitude = m_maxPower;
            }
            yield return null;
        }
    }

    protected override void PowerupFunction()
    {
        m_powerCellAmount += m_cellIncreaseRate;
        if(m_powerCellAmount >= m_maxPowerCellAmount)
        {
            DischargeCell();
            GainMoney();
            m_powerCellAmount = 0;
        }
    }

    private void Start()
    {
        if(m_maxPower > 0)
        {
            m_maxPower = -m_maxPower;
        }
        if(m_vaccumIncreaseRate < 0)
        {
            m_vaccumIncreaseRate = -m_vaccumIncreaseRate;
        }
        StartCoroutine(VaccumStart());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var trash = other.gameObject.GetComponentInParent<Trash>();
        if (trash)
        {
            PowerupFunction();
            Destroy(trash.gameObject);
        }
    }
}
