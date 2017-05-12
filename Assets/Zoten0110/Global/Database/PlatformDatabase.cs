using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDatabase : IDatabase<PlatformDatabase.PlatformEntry>
{


    [System.Serializable]
    public class PlatformEntry : IDatabaseEntry
    {
        [SerializeField]
        private GameObject m_platform;

        public GameObject platform { get { return m_platform; } }
    }

    [SerializeField]
    private List<PlatformEntry> m_entries;

    public override List<PlatformEntry> entries
    {
        get
        {
            return m_entries;
        }
    }

#if UNITY_EDITOR 
    [SerializeField]
    private GameObject m_overridePlatform;

    public override void ResetOverrides()
    {
        m_overridePlatform = null;
    }

    public override void Clear()
    {
        m_entries = new List<PlatformEntry>();
    }

    public void UpdateEntries()
    {
        
    }
#endif
}
