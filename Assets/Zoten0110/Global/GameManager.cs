using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GlobalGameSettings
{

}

public class GameManager : Singleton<GameManager>
{

    [SerializeField]
    private ISystem[] m_systemList;
    [Header("Global Game Settings")]
    [SerializeField]
    private GlobalGameSettings m_globalGameSettings;

    public T GetSystem<T>(string name = "") where T : ISystem
    {
        for (int i = 0; i < m_systemList.Length; i++)
        {
            var system = m_systemList[i];
            if (system.IsEqual(typeof(T), name))
            {
                if (system.isEnable)
                {
                    return (T)system;
                }
            }
        }

        Debug.LogWarning(typeof(T).ToString() + "  is not Found");
        return null;
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
