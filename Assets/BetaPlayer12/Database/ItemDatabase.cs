using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : IDatabase<ItemDatabase.ItemEntry>
{


    [System.Serializable]
    public class ItemEntry : IDatabaseEntry
    {
        [SerializeField]
        private GameObject m_item;

        public GameObject item { get { return m_item; } }
    }

    [SerializeField]
    private List<ItemEntry> m_entries;

    public override List<ItemEntry> entries
    {
        get
        {
            return m_entries;
        }
    }

#if UNITY_EDITOR 
    [SerializeField]
    private GameObject m_overrideItem;

    public override void ResetOverrides()
    {
        m_overrideItem = null;
    }

    public override void Clear()
    {
        m_entries = new List<ItemEntry>();
    }
#endif
}
