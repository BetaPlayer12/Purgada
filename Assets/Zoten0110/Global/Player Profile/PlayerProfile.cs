using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerInfo
{
    public int currentMoney;
    public int[] ownedTokens;
    public int quiExurgaLevel;
    public int droceoDroneLevel;
    public int orbRepairLevel;
}

public class PlayerProfile : ISystem {

    [System.Serializable]
    public class PowerupInfo
    {
        [SerializeField]
        private PowerupLevel m_level = PowerupLevel.Level1;
        private int m_maxLevel = 0;
        public PowerupLevel level { get { return m_level; } }
        public bool isMaxed { get { return (int)m_level == m_maxLevel; } }


        public PowerupInfo(int maxLevel =-1)
        {
            m_maxLevel = maxLevel == -1 ? (int)PowerupLevel._Count -1 : maxLevel - 1;

        }

        public void Upgrade()
        {
            if (m_level + 1 == PowerupLevel._Count)
            {
                return;
            }
            m_level += 1;
        }

        public void Set(int level)
        {
            m_level = (PowerupLevel)level;
        }

        public void Set(PowerupLevel level)
        {
            m_level = level;
        }
    }

    [SerializeField]
    private PlayerMoney m_playerMoney;
    [SerializeField] //Debug
    private List<TokenTypes> m_ownedTokens;
    [SerializeField]
    private PowerupInfo m_quiExurgaData = new PowerupInfo();
    [SerializeField]
    private PowerupInfo m_orbRepairData = new PowerupInfo();
    [SerializeField]
    private PowerupInfo m_droceoDroneData = new PowerupInfo();

    public PowerupInfo quiExurgaData { get { return m_quiExurgaData; } }
    public PowerupInfo orbRepairData { get { return m_orbRepairData; } }
    public PowerupInfo droceoDroneData { get { return m_droceoDroneData; } }

    public PowerupLevel GetCurrentPowerupLevel(IPowerup.Type type) {

        switch (type)
        {
            case IPowerup.Type.Droceo_Drone:
                return m_droceoDroneData.level;
            case IPowerup.Type.Orb_Repair:
                return m_orbRepairData.level;
            case IPowerup.Type.QuiExurga:
                return m_quiExurgaData.level;
        }
        return PowerupLevel._Count;
    }

    public void UpgradePowerup(IPowerup.Type type)
    {
        switch (type)
        {
            case IPowerup.Type.Droceo_Drone:
                 m_droceoDroneData.Upgrade();
                break;
            case IPowerup.Type.Orb_Repair:
                 m_orbRepairData.Upgrade();
                break;
            case IPowerup.Type.QuiExurga:
                 m_quiExurgaData.Upgrade();
                break;
        }
    }

    public bool IsPowerupMaxed(IPowerup.Type type)
    {
        switch (type)
        {
            case IPowerup.Type.Droceo_Drone:
                return m_droceoDroneData.isMaxed;
            case IPowerup.Type.Orb_Repair:
                return m_orbRepairData.isMaxed;
            case IPowerup.Type.QuiExurga:
                return m_quiExurgaData.isMaxed;
        }
        return false;
    }

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

    public void ResetTokens()
    {
        m_ownedTokens.Clear();
    }

    public PlayerInfo Save()
    {
        PlayerInfo newSave = new PlayerInfo();

        newSave.currentMoney = m_playerMoney.currentMoney;
        newSave.ownedTokens = new int[(int)TokenTypes._Count];

        //Enum to int
        {
            for (int i = 0; i < newSave.ownedTokens.Length; i++)
            {
                newSave.ownedTokens[i] = 0; // False
            }

            for (int i = 0; i < m_ownedTokens.Count; i++)
            {
                newSave.ownedTokens[(int)m_ownedTokens[i]] = 1; // True
            }
        }

        newSave.quiExurgaLevel = (int)m_quiExurgaData.level;
        newSave.droceoDroneLevel = (int)m_droceoDroneData.level;
        newSave.orbRepairLevel = (int)m_orbRepairData.level;

        return newSave;
    }

    public void Load(PlayerInfo newInfo)
    {
        m_playerMoney.SetMoney(newInfo.currentMoney);

        if (m_ownedTokens == null)
        {
            m_ownedTokens = new List<TokenTypes>();
        }

        for (int i = 0; i < newInfo.ownedTokens.Length; i++)
        {
            if(newInfo.ownedTokens[i] == 1)
            {
                m_ownedTokens.Add((TokenTypes)i);
            }
        }

        m_quiExurgaData.Set(newInfo.quiExurgaLevel);
        m_droceoDroneData.Set(newInfo.droceoDroneLevel);
        m_orbRepairData.Set(newInfo.orbRepairLevel);
    }

    public PlayerMoney playerMoney { get { return m_playerMoney; } }


    private void OnEnable()
    {
        this.RaiseSystemEventGlobal<LoadProfileEvent>(new LoadProfileEvent());
    }

    private void OnDisable()
    {
        this.RaiseSystemEventGlobal<SaveProfileEvent>(new SaveProfileEvent());
    }
}
