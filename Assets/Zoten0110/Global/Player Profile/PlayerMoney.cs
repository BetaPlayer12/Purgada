using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{

    [SerializeField]
    private int m_startingMoney;
    [SerializeField]
    private int m_currentMoney;
    private bool m_isSet = false;

    public int currentMoney { get { return m_currentMoney; } }
    public int startingMoney { get { return m_startingMoney; } }

    public void AddMoney(int value) =>
        m_currentMoney += value;

    public void SetMoney(int value)
    {
        m_currentMoney = value;
        m_isSet = true;
    }

    public void DeductMoney(int value) =>
        m_currentMoney -= value;

    public bool CanAfford(int value) =>
    m_currentMoney >= value;

    void Start()
    {
        if (m_isSet == false)
        {
            m_currentMoney = m_startingMoney;
        }
    }

}
