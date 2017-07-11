using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemEvent
{

}

public class SystemEventMessenger : Singleton<SystemEventMessenger>
{

    public delegate void EventDelegate<T>(T e) where T : SystemEvent;

    readonly Dictionary<Type, Delegate> m_delegates = new Dictionary<Type, Delegate>();

    public void AddListener<T>(EventDelegate<T> listener) where T : SystemEvent
    {
        Delegate _delegate;
        if (m_delegates.TryGetValue(typeof(T), out _delegate))// checks if the type of Game Event already exists
        {
            m_delegates[typeof(T)] = Delegate.Combine(_delegate, listener); //Chains the functions together
        }
        else
        {
            m_delegates[typeof(T)] = listener; //Adds Listener to Dictionary
        }
    }

    public void RemoveListener<T>(EventDelegate<T> listener) where T : SystemEvent
    {
        Delegate m_delegate;
        if (m_delegates.TryGetValue(typeof(T), out m_delegate)) // checks if the type of Game Event exists
        {
            Delegate currentDel = Delegate.Remove(m_delegate, listener); // Remove Attempt

            if (currentDel == null)//Attempt is successful thus it can delete backup;
            {
                m_delegates.Remove(typeof(T));
            }
            else
            {
                m_delegates[typeof(T)] = currentDel; //Attempt is NOT successful thus it assigns backup;
            }
        }
    }

    public void Raise<T>(T e) where T : SystemEvent
    {
        if (e == null)
        {
            throw new ArgumentNullException("e");
        }

        Delegate m_delegate;
        if (m_delegates.TryGetValue(typeof(T), out m_delegate)) //Checks if Event exists
        {
            EventDelegate<T> callback = m_delegate as EventDelegate<T>;
            if (callback != null)
            {
                callback(e); //Raises Event
            }
        }
    }
}
