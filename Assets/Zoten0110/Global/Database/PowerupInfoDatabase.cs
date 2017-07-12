using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerupLevel
{
    Level1,
    Level2,
    _Count
}

public class PowerupInfoDatabase : ScriptableObject {

    [System.Serializable]
    public class PowerupInfo
    {
        [SerializeField]
        private float m_duration;
        [SerializeField]
        private int m_cost;

        public float duration { get { return m_duration; } }
        public int cost { get { return m_cost; } }

        public void Set(PowerupInfo newInfo)
        {
            m_duration = duration;
            m_cost = cost;
        }
    }

    [SerializeField]
    private PowerupInfo[] m_infoList = new PowerupInfo[(int)PowerupLevel._Count];

    public float GetDuration(PowerupLevel level)
    {
        return m_infoList[(int)level].duration;
    }

    public int GetCost(PowerupLevel level)
    {
        return m_infoList[(int)level].cost;
    }

    public void UpdateList()
    {
        PowerupInfo[] newList = new PowerupInfo[(int)PowerupLevel._Count];

        for (int i = 0; i < m_infoList.Length; i++)
        {
            newList[i].Set(m_infoList[i]);
        }

        m_infoList = newList;
    }
}
