using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalSystemEventExtention {

    private static SystemEventMessenger GlobalMessenger = SystemEventMessenger.Instance;
    private static Dictionary<GameObject, SystemEventMessenger> Messengers = new Dictionary<GameObject, SystemEventMessenger>();
    private static readonly bool LOG_ENABLED = false;

    #region GameObject
    public static void RaiseSystemEvent<T>(this GameObject obj, T e) where T : SystemEvent
    {
        SystemEventMessenger em;
        if (!Messengers.TryGetValue(obj, out em))
        {
            em = new SystemEventMessenger();
            Messengers[obj] = em;
        }
        em.Raise<T>(e);

        if (GlobalSystemEventExtention.LOG_ENABLED)
        {
            Debug.Log(obj.name + "<" + typeof(T).Name + "> Event raised");
        }
    }

    public static void RaiseSystemEventGlobal<T>(this GameObject obj, T e) where T : SystemEvent
    {
        GlobalMessenger.Raise<T>(e);
    }

    public static void AddSystemEventListener<T>(this GameObject obj, SystemEventMessenger.EventDelegate<T> del) where T : SystemEvent
    {
        SystemEventMessenger em;
        if (!Messengers.TryGetValue(obj, out em))
        {
            em = new SystemEventMessenger();
            Messengers[obj] = em;
        }
        em.AddListener<T>(del);

        if (GlobalSystemEventExtention.LOG_ENABLED)
        {
            Debug.Log(obj.name + "<" + typeof(T).Name + "> Listener added [" + del.Target.GetType().Name + " " + del.Method.Name + "]");
        }
    }

    public static void AddSystemEventListenerGlobal<T>(this GameObject obj, SystemEventMessenger.EventDelegate<T> del) where T : SystemEvent
    {
        GlobalMessenger.AddListener<T>(del);
    }

    public static void RemoveSystemEventListener<T>(this GameObject obj, SystemEventMessenger.EventDelegate<T> del) where T : SystemEvent
    {
        SystemEventMessenger em;
        if (!Messengers.TryGetValue(obj, out em)) return;


        em.RemoveListener<T>(del);

        if (GlobalSystemEventExtention.LOG_ENABLED)
        {
            Debug.Log(obj.name + "<" + typeof(T).Name + "> Listener removed [" + del.Target.GetType().Name + " " + del.Method.Name + "]");
        }
    }

    public static void RemoveSystemEventListeners(this GameObject obj)
    {
        Messengers.Remove(obj);

        if (GlobalSystemEventExtention.LOG_ENABLED)
        {
            Debug.Log(obj.name + "<???> All listeners removed");
        }
    }

    public static void RemoveSystemEventListenerGlobal<T>(this GameObject obj, SystemEventMessenger.EventDelegate<T> del) where T : SystemEvent
    {
        GlobalMessenger.RemoveListener<T>(del);
    }
    #endregion

    #region MonoBehaviour
    public static void RaiseSystemEvent<T>(this MonoBehaviour obj, T e) where T : SystemEvent
    {
        GameObject go = obj.gameObject;
        go.RaiseSystemEvent<T>(e);
    }

    public static void RaiseSystemEventGlobal<T>(this MonoBehaviour obj, T e) where T : SystemEvent
    {
        GlobalMessenger.Raise<T>(e);
    }

    public static void AddSystemEventListener<T>(this MonoBehaviour obj, SystemEventMessenger.EventDelegate<T> del) where T : SystemEvent
    {
        GameObject go = obj.gameObject;
        go.AddSystemEventListener<T>(del);
    }

    public static void AddSystemEventListenerGlobal<T>(this MonoBehaviour obj, SystemEventMessenger.EventDelegate<T> del) where T : SystemEvent
    {
        GlobalMessenger.AddListener<T>(del);
    }

    public static void RemoveSystemEventListener<T>(this MonoBehaviour obj, SystemEventMessenger.EventDelegate<T> del) where T : SystemEvent
    {
        GameObject go = obj.gameObject;
        go.RemoveSystemEventListener<T>(del);
    }

    public static void RemoveSystemEventListeners(this MonoBehaviour obj)
    {
        Messengers.Remove(obj.gameObject);
    }

    public static void RemoveSystemEventListenerGlobal<T>(this MonoBehaviour obj, SystemEventMessenger.EventDelegate<T> del) where T : SystemEvent
    {
        GlobalMessenger.RemoveListener<T>(del);
    }
    #endregion
}
