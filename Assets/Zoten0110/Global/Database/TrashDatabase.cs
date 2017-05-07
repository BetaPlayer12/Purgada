using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashDatabase : IDatabase<TrashDatabase.TrashEntry>
{


    [System.Serializable]
    public class TrashEntry : IDatabaseEntry
    {
        [SerializeField]
        private GameObject m_trash;
        [SerializeField]
        private Trash.Type m_type;


        public GameObject trash { get { return m_trash; } }
        public Trash.Type type { get { return m_type; } }
    }

    [SerializeField]
    private List<TrashEntry> m_entries;
    private TrashEntry[] m_organicTrash;
    private TrashEntry[] m_toxicTrash;
    private TrashEntry[] m_recyclableTrash;

    public override List<TrashEntry> entries
    {
        get
        {
            return m_entries;
        }
    }

#if UNITY_EDITOR 
    [SerializeField]
    private GameObject m_overrideTrash;
    [SerializeField]
    private Trash.Type m_overrideType;

    public override void ResetOverrides()
    {
        m_overrideTrash = null;
    }

    public override void Clear()
    {
        m_entries = new List<TrashEntry>();
    }

    public void UpdateSeperateEntries()
    {
        int organicSize = 0;
        int toxicSize = 0;
        int recyclableSize = 0;

        for (int i = 0; i < m_entries.Count; i++)
        {
            switch (entries[i].type)
            {
                case Trash.Type.Organic:
                    organicSize++;
                    break;
                case Trash.Type.Toxic:
                    toxicSize++;
                    break;
                case Trash.Type.Recyclable:
                    recyclableSize++;
                    break;
            }
        }

        m_organicTrash = new TrashEntry[organicSize];
        m_toxicTrash = new TrashEntry[toxicSize];
        m_recyclableTrash = new TrashEntry[recyclableSize];

        int organicCounter = 0;
        int toxicCounter = 0;
        int recyclableCounter = 0;
        for (int i = 0; i < m_entries.Count; i++)
        {
            switch (entries[i].type)
            {
                case Trash.Type.Organic:
                    m_organicTrash[organicCounter] = entries[i];
                    organicCounter++;
                    break;
                case Trash.Type.Toxic:
                    m_toxicTrash[toxicCounter] = entries[i];
                    toxicCounter++;
                    break;
                case Trash.Type.Recyclable:
                    m_recyclableTrash[recyclableCounter] = entries[i];
                    recyclableCounter++;
                    break;
            }
        }
    }

    public void UpdateTrashComponents()
    {
        for (int i = 0; i < m_organicTrash.Length; i++)
        {
            var entry = m_organicTrash[i];

            entry.trash.GetComponent<Trash>().SetInfo(entry.type, entry.ID);
        }

        for (int i = 0; i < m_toxicTrash.Length; i++)
        {
            var entry = m_toxicTrash[i];

            entry.trash.GetComponent<Trash>().SetInfo(entry.type, entry.ID);
        }

        for (int i = 0; i < m_recyclableTrash.Length; i++)
        {
            var entry = m_recyclableTrash[i];

            entry.trash.GetComponent<Trash>().SetInfo(entry.type, entry.ID);
        }
    }
#endif

    public GameObject GetRandomOrganicTrash() =>
        m_organicTrash[Random.Range(0, m_organicTrash.Length)].trash;

    public GameObject GetRandomToxicTrash() =>
    m_toxicTrash[Random.Range(0, m_toxicTrash.Length)].trash;

    public GameObject GetRandomRecyclableTrash() =>
    m_recyclableTrash[Random.Range(0, m_recyclableTrash.Length)].trash;
}
