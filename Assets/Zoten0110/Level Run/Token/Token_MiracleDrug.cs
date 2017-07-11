using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token_MiracleDrug : IToken
{

    [SerializeField]
    private PlayerHealth m_playerHealth;


    public override TokenTypes type { get { return TokenTypes.Miracle_Drug; } }

    private void OnPlayerDamageEvent(PlayerDamageEvent e)
    {
        if (m_isActive)
        {
            if (m_playerHealth.currentHealth <= 0)
            {
                m_playerHealth.Heal(float.MaxValue);
            }
        }
    }

    protected override void OnLoadModule()
    {
        this.AddGameEventListenerGlobal<PlayerDamageEvent>(OnPlayerDamageEvent);
    }


    protected override void OnUnloadModule()
    {
        this.RemoveGameEventListenerGlobal<PlayerDamageEvent>(OnPlayerDamageEvent);
    }
}
