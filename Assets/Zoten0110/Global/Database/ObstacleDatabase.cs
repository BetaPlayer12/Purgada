using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDatabase : IDatabase<ObstacleDatabase.ObstacleEntry>
{

    [System.Serializable]
    public class ObstacleEntry : IDatabaseEntry
    {
        [SerializeField]
        private GameObject m_obstacle;
        [SerializeField]
        private bool m_entityInstantiated; //Spawned by other characters not by platform


        public GameObject obstacle { get { return m_obstacle; } }
        public bool entityInstantiated { get { return m_entityInstantiated; } }
    }

    [SerializeField]
    private List<ObstacleEntry> m_entries;
    private ObstacleEntry[] m_nonEntityInstantiatedEntries;


    public override List<ObstacleEntry> entries
    {
        get
        {
            return m_entries;
        }
    }

#if UNITY_EDITOR 
    [SerializeField]
    private GameObject m_overrideObstacle;
    [SerializeField]
    private bool m_overrideEntityInstantiated;

    public override void ResetOverrides()
    {
        m_overrideObstacle = null;
        m_overrideEntityInstantiated = false;
    }

    public override void Clear()
    {
        m_entries = new List<ObstacleEntry>();
    }

    public void UpdateSeperateEntries()
    {
        int nonEntityInstantiatedSize = 0;

        for (int i = 0; i < m_entries.Count; i++)
        {
            if (m_entries[i].entityInstantiated == false)
            {
                nonEntityInstantiatedSize++;
            }
        }

        int nonEntityInstantiatedCounter = 0;

        m_nonEntityInstantiatedEntries = new ObstacleEntry[nonEntityInstantiatedSize];

        for (int i = 0; i < m_entries.Count; i++)
        {
            if (m_entries[i].entityInstantiated == false)
            {
                m_nonEntityInstantiatedEntries[nonEntityInstantiatedCounter] = entries[i];
                nonEntityInstantiatedCounter++;
            }
        }
    }
#endif

    public GameObject GetRandomObstacle() =>
        m_nonEntityInstantiatedEntries[Random.Range(0, m_nonEntityInstantiatedEntries.Length)].obstacle;

    public GameObject GetObstacle(int ID)
    {
        for (int i = 0; i < m_nonEntityInstantiatedEntries.Length; i++)
        {
            if (m_nonEntityInstantiatedEntries[i].ID == ID)
            {
                return m_nonEntityInstantiatedEntries[i].obstacle;
            }
        }
        return null;
    }

    public GameObject GetObstacle(string Name)
    {
        return m_entries[GetIndex(name)].obstacle;
    }
}
