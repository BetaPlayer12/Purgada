using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfile : ISystem {

    public class PowerupInfoDatabase
    {
        [SerializeField]
        private PowerupLevel m_level = PowerupLevel.Level1;
        private int m_maxLevel = 0;
        public PowerupLevel level { get { return m_level; } }
        public bool isMaxed { get { return (int)m_level == m_maxLevel; } }


        public PowerupInfoDatabase(int maxLevel =-1)
        {
            m_maxLevel = maxLevel == -1 ? (int)PowerupLevel._Count : maxLevel;

        }

        public void Upgrade()
        {
            if (m_level + 1 == PowerupLevel._Count)
            {
                return;
            }
            m_level += 1;
        }

    }

    [SerializeField]
    private PlayerMoney m_playerMoney;
    [SerializeField] //Debug
    private List<TokenTypes> m_ownedTokens;
    [SerializeField]
    private PowerupInfoDatabase m_quiExurgaData = new PowerupInfoDatabase();
    [SerializeField]
    private PowerupInfoDatabase m_orbRepairData = new PowerupInfoDatabase();
    [SerializeField]
    private PowerupInfoDatabase m_droceoDroneData = new PowerupInfoDatabase();

    public PowerupInfoDatabase quiExurgaData { get { return m_quiExurgaData; } }
    public PowerupInfoDatabase orbRepairData { get { return m_orbRepairData; } }
    public PowerupInfoDatabase droceoDroneData { get { return m_droceoDroneData; } }

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
