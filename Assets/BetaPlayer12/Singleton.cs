using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour  where T: class{
    protected static T m_instance = null;

    public static T Instance
    {
        get
        {
            if (m_instance == null)
            {
                var instance = FindObjectOfType(typeof(T)) as Singleton<T>;

                m_instance = instance as T;
            }

            return m_instance;
        }
    }

    void OnDestroy()
    {
        m_instance = null;
    }
}
