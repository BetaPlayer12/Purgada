using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DatabaseNotFoundException : Exception
{
    public DatabaseNotFoundException()
    {
    }

    public DatabaseNotFoundException(string message)
        : base(message)
    {
    }

    public DatabaseNotFoundException(string message, Exception inner)
        : base(message, inner)
    {
    }
}

public class DatabaseSystem : ISystem
{
    //TODO: Implicitly Declare variables of Databases and Store them to the IBaseDatabase array
    [SerializeField]
    private TrashDatabase m_trashDatabase;
    [SerializeField]
    private ObstacleDatabase m_obstacleDatabase;
    [SerializeField]
    private PlatformDatabase m_platformDatabse;

    private IBaseDatabase[] m_databases;

    public T GetDatabase<T>() where T : IBaseDatabase
    {
        foreach (IBaseDatabase database in m_databases)
        {

            if (database.GetType() == typeof(T))
                return (T)database;
        }
        throw new DatabaseNotFoundException();
    }

    public int GetSize<T>() where T : IBaseDatabase
    {
        return GetDatabase<T>().Count;
    }

    public T GetEntryOf<T>(int ID) where T : IDatabaseEntry
    {
        foreach (IBaseDatabase database in m_databases)
        {
            if (database[0].GetType() == typeof(T))
            {
                return (T)database.GetIEntry(ID);
            }
        }
        throw new EntryNotFoundException();
    }

    public T GetEntryOf<T>(string entryname) where T : IDatabaseEntry
    {
        foreach (IBaseDatabase database in m_databases)
        {
            if (database[0].GetType() == typeof(T) && database.IsDatabase(name))
            {
                return (T)database.GetIEntry(entryname);
            }
        }
        throw new EntryNotFoundException();
    }

    public T GetEntryOf<T>(int ID,string name) where T : IDatabaseEntry
    {
        foreach (IBaseDatabase database in m_databases)
        {
            if (database[0].GetType() == typeof(T) && database.IsDatabase(name))
            {
                return (T)database.GetIEntry(ID);
            }
        }
        throw new EntryNotFoundException();
    }

    public int Count()
    {
        return m_databases.Length;
    }

    void Awake()
    {
        m_databases = new IBaseDatabase[] { m_trashDatabase, m_obstacleDatabase, m_platformDatabse };
    }
}