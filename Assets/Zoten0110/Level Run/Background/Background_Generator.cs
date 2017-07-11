using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBackgroundEvent : GameEvent
{
    public CreateBackgroundEvent(GameObject senderObj) : base(senderObj)
    {
        sender = senderObj;
    }
}

public class Background_Generator : MonoBehaviour {

    [SerializeField]
    private Transform m_parent;
    [SerializeField]
    private GameObject m_bgInstance;
    public float m_spawnScale;

    private void CreateBackground()
    {
        var instance = Instantiate(m_bgInstance, m_parent) as GameObject;
        var instanceTransform = instance.transform;
        instanceTransform.localPosition = Vector3.zero;
        instanceTransform.localScale = Vector3.one * m_spawnScale;

        instance.GetComponent<ConstantMovement>().Move();
    }

    private void OnCreateBackgroundEvent(CreateBackgroundEvent e)
    {
        CreateBackground();
    }

    void OnEnable()
    {
        this.AddGameEventListenerGlobal<CreateBackgroundEvent>(OnCreateBackgroundEvent);
    }

    void OnDisable()
    {
        this.RemoveGameEventListenerGlobal<CreateBackgroundEvent>(OnCreateBackgroundEvent);
    }
}
