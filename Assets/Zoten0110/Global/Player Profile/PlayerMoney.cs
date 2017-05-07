using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{

    [SerializeField]
    private int m_startingMoney;
    [SerializeField]
    private int m_currentMoney;

    public int currentMoney { get { return m_currentMoney; } }

    public void AddMoney(int value) =>
        m_currentMoney += value;

    public void DeductMoney(int value) =>
        m_currentMoney -= value;

    public bool CanAfford(int value) =>
    m_currentMoney >= value;

}
