using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token_Sponsorship : IToken {

    [SerializeField]
    private int m_moneyFactor =1;

    public override TokenTypes type { get { return TokenTypes.Sponsorship; } }

    public override void MakeActive()
    {
        base.MakeActive();
        if (m_isActive)
        {
            LevelRunMoneyHandler.Instance.SetMoneyFactor(m_moneyFactor);
        }
    }
}
