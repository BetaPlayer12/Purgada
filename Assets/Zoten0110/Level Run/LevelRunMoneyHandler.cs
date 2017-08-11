using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRunMoneyHandler : Singleton<LevelRunMoneyHandler> {

    private PlayerMoney m_playerMoney;
    [SerializeField]
    private int m_baseTrashMoney;
    [SerializeField]
    private int m_unsuccessfulDisposalMoney;
    private int m_moneyFactor =1;

    public void SetMoneyFactor(int factor)
    {
        m_moneyFactor = factor;
    }

    public void GiveMoney()
    {
        m_playerMoney.AddMoney(m_baseTrashMoney * m_moneyFactor);

    }

    public void DeductMoney()
    {
        m_playerMoney.DeductMoney(m_unsuccessfulDisposalMoney);
    }

    public void ShowMoney(bool success, Vector3 screenPos)
    {
        this.RaiseGameEventGlobal<MoneyEvent>(new MoneyEvent(gameObject, success ? (m_baseTrashMoney * m_moneyFactor) : -m_unsuccessfulDisposalMoney, screenPos));
    }

    void Start()
    {
        m_playerMoney = GameManager.Instance.GetSystem<PlayerProfile>().playerMoney;
    }
}
