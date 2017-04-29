using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Exception thrown when the entry is not found
/// </summary>
public class EntryNotFoundException : Exception {
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

/// <summary>
/// Base Class for Database
/// </summary>
public abstract class IDatabase : ScriptableObject {

    [SerializeField]
    private string m_name;

    [System.Serializable]
    public class IEntry
    {
        [SerializeField][Tooltip("ID of the Entry")]
        protected int m_iD;
        [SerializeField][Tooltip("Name of the Entry")]
        protected string m_name;

        public int iD { get { return m_iD; } }
        public string name { get { return m_name; } }
    }

    public abstract List<IEntry> entries { get; }
    public int Count { get { return entries.Count; } }
    public IEntry this[int i] { get { return entries[i]; } }

#if UNITY_EDITOR //Only accessible by the editor
    public int m_overrideID;
    public string m_overrideName;

    public void ResetOverride(int ID)
    {
        m_overrideID = ID;
        m_overrideName = "";
        AdditionalReset();
    }

    /// <summary>
    /// Addition code for Resetting used by child classes
    /// </summary>
    protected abstract void AdditionalReset();

    public abstract void Clear();

    public bool isInDatabase(string name, int i = -1)
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

    public void SortID() { entries.Sort((x, y) => (x.iD.CompareTo(y.iD))); }
    public void SortName() { entries.Sort((x, y) => (x.name.CompareTo(y.name))); }

    public int GetIndex(int instanceID)
    {
        SortID();

        var minIndex = 0;
        var maxIndex = Count - 1;

        // Binary search
        while (maxIndex >= minIndex)
        {
            int mid = (minIndex + maxIndex) / 2;
            if (instanceID < entries[mid].iD)
                maxIndex = mid - 1;
            else if (instanceID > entries[mid].iD)
                minIndex = mid + 1;
            else
            {
                return mid;
            }
        }
        Debug.Log("Entry does not exists");
        throw new EntryNotFoundException();
    }

    public int GetIndex(string name) {
        SortName();

        var minIndex = 0;
        var maxIndex = Count - 1;

        // Binary search
        while (maxIndex >= minIndex)
        {
            int mid = (minIndex + maxIndex) / 2;
            if (name.CompareTo(entries[mid].name)<0)
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

    public IEntry GetIEntry(int instanceID)
    {
        var index = GetIndex(instanceID);
        return entries[index];
    }

    public bool IsName(string name) =>
        m_name == name;

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
}
