using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfile : ISystem {

    [SerializeField]
    private PlayerMoney m_playerMoney;
    [SerializeField] //Debug
    private List<TokenTypes> m_ownedTokens;

    public void AddToken(TokenTypes type)
    {
        for (int i = 0; i < m_ownedTokens.Count; i++)
        {
            if (m_ownedTokens[i] == type)
                return;
        }

        m_ownedTokens.Add(type);
    }

    public bool isTokenOwned(TokenTypes type)
    {
        for (int i = 0; i < m_ownedTokens.Count; i++)
        {
            if (m_ownedTokens[i] == type)
                return true;
        }

        return false;
    }

    public PlayerMoney playerMoney { get { return m_playerMoney; } }

}
