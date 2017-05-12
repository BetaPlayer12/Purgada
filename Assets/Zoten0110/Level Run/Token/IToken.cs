using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IToken : MonoBehaviour
{
    protected bool m_isActive;

    protected abstract void OnLevelStart(LevelStartEvent e);

    void OnEnable()
    {
        this.AddEventListenerGlobal<LevelStartEvent>(OnLevelStart);
    }

    void OnDisable()
    {
        this.RemoveEventListenerGlobal<LevelStartEvent>(OnLevelStart);
    }

}
	
