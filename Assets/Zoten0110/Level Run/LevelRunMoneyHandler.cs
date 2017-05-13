using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRunMoneyHandler : Singleton<LevelRunMoneyHandler> {

    private PlayerMoney m_playerMoney;
    [SerializeField]
    private int m_baseTrashMoney;
    private int m_moneyFactor =1;

    public void SetMoneyFactor(int factor)
    {
        m_moneyFactor = factor;
    }

    public void GiveMoney()
    {
        m_playerMoney.AddMoney(m_baseTrashMoney * m_moneyFactor);
    }

    void Start()
    {
        m_playerMoney = GameManager.Instance.GetSystem<PlayerProfile>().playerMoney;
    }
}
