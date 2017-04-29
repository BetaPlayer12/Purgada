using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : IDatabase
{

    [System.Serializable]
    public class Entry : IEntry
    {
        [SerializeField]
        private GameObject m_item;

        public GameObject item { get { return m_item; } }
    }

    [SerializeField]
    private List<Entry> m_itemDatabase = new List<Entry>();

    public override List<IEntry> entries
    {
        get
        {
            return m_itemDatabase.ConvertAll((x) => (IEntry)x);
        }
    }

#if UNITY_EDITOR
    [SerializeField]
    private GameObject m_overrideItem;

    public override void Clear()
    {
        m_itemDatabase = new List<Entry>();
    }

    protected override void AdditionalReset()
    {
        m_overrideItem = null;
    }
#endif

    public GameObject GetItem(int id) =>
        ((Entry)GetIEntry(id)).item;

}
