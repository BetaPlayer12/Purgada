using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalGameEventExtention {

    private static GameEventMessenger GlobalMessenger = GameEventMessenger.Instance;
    private static Dictionary<GameObject, GameEventMessenger> Messengers = new Dictionary<GameObject, GameEventMessenger>();
    private static readonly bool LOG_ENABLED = false;

    #region GameObject
    public static void RaiseGameEvent<T>(this GameObject obj, T e) where T : GameEvent
    {
        GameEventMessenger em;
        if (!Messengers.TryGetValue(obj, out em))
        {
            em = new GameEventMessenger();
            Messengers[obj] = em;
        }
        em.Raise<T>(e);

        if (GlobalGameEventExtention.LOG_ENABLED)
        {
            Debug.Log(obj.name + "<" + typeof(T).Name + "> Event raised");
        }
    }

    public static void RaiseGameEventGlobal<T>(this GameObject obj, T e) where T : GameEvent
    {
        GlobalMessenger.Raise<T>(e);
    }

    public static void AddGameEventListener<T>(this GameObject obj, GameEventMessenger.EventDelegate<T> del) where T : GameEvent
    {
        GameEventMessenger em;
        if (!Messengers.TryGetValue(obj, out em))
        {
            em = new GameEventMessenger();
            Messengers[obj] = em;
        }
        em.AddListener<T>(del);

        if (GlobalGameEventExtention.LOG_ENABLED)
        {
            Debug.Log(obj.name + "<" + typeof(T).Name + "> Listener added [" + del.Target.GetType().Name + " " + del.Method.Name + "]");
        }
    }

    public static void AddGameEventListenerGlobal<T>(this GameObject obj, GameEventMessenger.EventDelegate<T> del) where T : GameEvent
    {
        GlobalMessenger.AddListener<T>(del);
    }

    public static void RemoveGameEventListener<T>(this GameObject obj, GameEventMessenger.EventDelegate<T> del) where T : GameEvent
    {
        GameEventMessenger em;
        if (!Messengers.TryGetValue(obj, out em)) return;


        em.RemoveListener<T>(del);

        if (GlobalGameEventExtention.LOG_ENABLED)
        {
            Debug.Log(obj.name + "<" + typeof(T).Name + "> Listener removed [" + del.Target.GetType().Name + " " + del.Method.Name + "]");
        }
    }

    public static void RemoveGameEventListeners(this GameObject obj)
    {
        Messengers.Remove(obj);

        if (GlobalGameEventExtention.LOG_ENABLED)
        {
            Debug.Log(obj.name + "<???> All listeners removed");
        }
    }

    public static void RemoveGameEventListenerGlobal<T>(this GameObject obj, GameEventMessenger.EventDelegate<T> del) where T : GameEvent
    {
        GlobalMessenger.RemoveListener<T>(del);
    }
    #endregion

    #region MonoBehaviour
    public static void RaiseGameEvent<T>(this MonoBehaviour obj, T e) where T : GameEvent
    {
        GameObject go = obj.gameObject;
        go.RaiseGameEvent<T>(e);
    }

    public static void RaiseGameEventGlobal<T>(this MonoBehaviour obj, T e) where T : GameEvent
    {
        GlobalMessenger.Raise<T>(e);
    }

    public static void AddGameEventListener<T>(this MonoBehaviour obj, GameEventMessenger.EventDelegate<T> del) where T : GameEvent
    {
        GameObject go = obj.gameObject;
        go.AddGameEventListener<T>(del);
    }

    public static void AddGameEventListenerGlobal<T>(this MonoBehaviour obj, GameEventMessenger.EventDelegate<T> del) where T : GameEvent
    {
        GlobalMessenger.AddListener<T>(del);
    }

    public static void RemoveGameEventListener<T>(this MonoBehaviour obj, GameEventMessenger.EventDelegate<T> del) where T : GameEvent
    {
        GameObject go = obj.gameObject;
        go.RemoveGameEventListener<T>(del);
    }

    public static void RemoveGameEventListeners(this MonoBehaviour obj)
    {
        Messengers.Remove(obj.gameObject);
    }

    public static void RemoveGameEventListenerGlobal<T>(this MonoBehaviour obj, GameEventMessenger.EventDelegate<T> del) where T : GameEvent
    {
        GlobalMessenger.RemoveListener<T>(del);
    }
    #endregion
}
