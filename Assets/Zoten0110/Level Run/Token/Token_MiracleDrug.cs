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

    void OnEnable()
    {
        this.AddEventListenerGlobal<PlayerDamageEvent>(OnPlayerDamageEvent);
    }

    void OnDisable()
    {
        this.RemoveEventListenerGlobal<PlayerDamageEvent>(OnPlayerDamageEvent);
    }
}
