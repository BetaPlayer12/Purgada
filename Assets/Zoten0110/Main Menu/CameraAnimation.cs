using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartEvent : GameEvent
{
    public LevelStartEvent(GameObject senderObj) : base(senderObj)
    {
        sender = senderObj;
    }
}

public class CameraAnimation : MonoBehaviour {


    private Animator m_animator;

	public void OnLevelRun(bool value)
    {
        m_animator.SetBool("Level Run", value);
    }

    public void StartLevel()
    {
        this.RaiseGameEventGlobal(new LevelStartEvent(gameObject));
    }

    void Awake()
    {
        m_animator = GetComponent<Animator>();
    }
}
