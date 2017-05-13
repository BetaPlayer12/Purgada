using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token_BoosterShot : IToken {

    [SerializeField]
    private PlayerHealth m_playerHealth;
    [SerializeField][Range(0f, 100f)]
    private float m_damageReduction;

    public override TokenTypes type { get { return TokenTypes.Booster_Shot; } }

    public override void MakeActive()
    {
        base.MakeActive();
        if (m_isActive)
        {
            m_playerHealth.SetDamageReduction(m_damageReduction);
        }
    }
}
