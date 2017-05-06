using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Exception thrown when the entry is not found
/// </summary>
public class EntryNotFoundException : Exception
{
    public EntryNotFoundException()
    {
    }

    public EntryNotFoundException(string message)
        : base(message)
    {
    }

    public EntryNotFoundException(string message, Exception inner)
        : base(message, inner)
    {
    }
}

[System.Serializable]
public class IDatabaseEntry
{
    [SerializeField]
    protected int m_ID;
    [SerializeField]
    protected string m_name;

    public int ID { get { return m_ID; } }
    public string name { get { return m_name; } }
}

public interface IBaseDatabase
{
    int Count { get; }
    IDatabaseEntry this[int i] { get; }
    IDatabaseEntry GetIEntry(int ID);
    IDatabaseEntry GetIEntry(string name);
    void Clear();
    void ResetOverrides();
    bool IsDatabase(string databaseName);
    bool IsInDatabase(string name, int i = -1);
    void SortID();
    void SortName();
};

/// <summary>
/// Base Class for Database
/// </summary>
public abstract class IDatabase<ListEntryT> : ScriptableObject, IBaseDatabase where ListEntryT
: IDatabaseEntry
{


    private enum OrderType
    {
        None,
        ID,
        Name,
    }

    [SerializeField]
    private string m_databaseName = "";
    private OrderType m_orderedBy = OrderType.None;

    public virtual List<ListEntryT> entries { get { return null; } }
    public int Count { get { return entries.Count; } }
    

#if UNITY_EDITOR //Only accessible by the editor
    public IDatabaseEntry this[int i] { get { return entries[i]; } }

    public abstract void ResetOverrides();

    public virtual void Clear() { }

    public bool IsInDatabase(string name, int i = -1)
    {
        if (i == -1)
        {
            for (i = 0; i < entries.Count; i++)
            {
                if (CompressString(entries[i].name) == CompressString(name)
                    || CompressString(name).Contains(CompressString(entries[i].name))
                    || CompressString(entries[i].name).Contains(CompressString(name)))
                    return true;
            }
        }
        else
        {
            if (CompressString(entries[i].name) == CompressString(name)
                   || CompressString(name).Contains(CompressString(entries[i].name))
                   || CompressString(entries[i].name).Contains(CompressString(name)))
                return true;
        }
        return false;
    }
#endif

    private string CompressString(string m_string)
    {
        string newString = "";
        var splitString = m_string.Split(' ');

        for (int i = 0; i < splitString.Length; i++)
        {
            newString += splitString[i].ToLower();
        }
        return newString;
    }

    public bool IsDatabase(string databaseName) =>
        m_databaseName == databaseName;

    public void SortID()
    {
        entries.Sort((x, y) => (x.ID.CompareTo(y.ID)));
        m_orderedBy = OrderType.ID;
    }
    public void SortName()
    {
        entries.Sort((x, y) => (x.name.CompareTo(y.name)));
        m_orderedBy = OrderType.Name;
    }

    public int GetIndex(int instanceID)
    {
        if (m_orderedBy != OrderType.ID)
        {
            SortID();
        }

        var minIndex = 0;
        var maxIndex = Count - 1;

        // Binary search
        while (maxIndex >= minIndex)
        {
            int mid = (minIndex + maxIndex) / 2;
            if (instanceID < entries[mid].ID)
                maxIndex = mid - 1;
            else if (instanceID > entries[mid].ID)
                minIndex = mid + 1;
            else
            {
                return mid;
            }
        }
        Debug.Log("Entry does not exists");
        throw new EntryNotFoundException();
    }

    public int GetIndex(string name)
    {
        if (m_orderedBy != OrderType.Name)
        {
            SortName();
        }

        var minIndex = 0;
        var maxIndex = Count - 1;

        // Binary search
        while (maxIndex >= minIndex)
        {
            int mid = (minIndex + maxIndex) / 2;
            if (name.CompareTo(entries[mid].name) < 0)
                maxIndex = mid - 1;
            else if (name.CompareTo(entries[mid].name) > 0)
                minIndex = mid + 1;
            else
            {
                return mid;
            }
        }
        Debug.Log("Entry does not exists");
        throw new EntryNotFoundException();
    }

    public IDatabaseEntry GetIEntry(int ID)
    {
        var index = GetIndex(ID);
        return entries[index];
    }

    public IDatabaseEntry GetIEntry(string name)
    {
        var index = GetIndex(name);
        return entries[index];
    }

    public ListEntryT GetEntry(int ID)
    {
        var index = GetIndex(ID);
        return entries[index];
    }

    public ListEntryT GetEntry(string name)
    {
        var index = GetIndex(name);
        return entries[index];
    }


}
