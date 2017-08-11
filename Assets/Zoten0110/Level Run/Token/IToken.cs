using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TokenTypes
{
    Miracle_Drug,
    Sponsorship,
    Booster_Shot,
    _Count
}

public abstract class IToken : MonoBehaviour
{
    protected bool m_isActive;

    public abstract TokenTypes type {get;}

    public virtual void MakeActive()
    {}


    private void OnLevelStartEvent(LevelStartEvent e)
    {
        m_isActive = GameManager.Instance.GetSystem<PlayerProfile>().isTokenOwned(type);
        MakeActive();
    }

    protected virtual void OnLoadModule(){ }
    protected virtual void OnUnloadModule() { }

    void OnEnable()
    {
        this.AddGameEventListenerGlobal<LevelStartEvent>(OnLevelStartEvent);
    }

    void OnDisable()
    {
        this.RemoveGameEventListenerGlobal<LevelStartEvent>(OnLevelStartEvent);
    }
}

