using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
 

public class DatabaseSystem : ISystem {

    [SerializeField]
    private IDatabase[] m_databaseLibrary;

    public T GetEntry<T>(int index,string databaseName = "") where T: IDatabase.IEntry
    {
        for (int i = 0; i < m_databaseLibrary.Length; i++)
        {
            var database = m_databaseLibrary[i];
            if (database.GetType() == typeof(T) && database.IsName(databaseName))
            {
                return (T)database.GetIEntry(index);
            }
        }

        throw new ArgumentException();
    }

    public int GetDatabaseSize<T>() where T : IDatabase.IEntry
    {
        for (int i = 0; i < m_databaseLibrary.Length; i++)
        {
            var database = m_databaseLibrary[i];
            if (database.GetType() == typeof(T))
            {
                return database.Count;
            }
        }

        throw new ArgumentException();
    }
}
