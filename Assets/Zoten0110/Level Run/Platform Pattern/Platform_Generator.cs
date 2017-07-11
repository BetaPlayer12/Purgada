using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlatformEvent : GameEvent
{
    public CreatePlatformEvent(GameObject senderObj) : base(senderObj)
    {
        sender = senderObj;
    }
}

public class Platform_Generator : MonoBehaviour {

    [SerializeField]
    private Transform m_parent;
    private LevelConstructor m_levelConstructor;
    public float m_spawnScale;

    private void CreatePlatform()
    {
        var instance = Instantiate(m_levelConstructor.GetPlatform(), m_parent) as GameObject;
        var instanceTransform = instance.transform;
        instanceTransform.localPosition = Vector3.zero;
        instanceTransform.localScale = Vector3.one * m_spawnScale;

        instance.GetComponent<ConstantMovement>().Move();
    }

    private void OnCreatePlatformEvent(CreatePlatformEvent e)
    {
        CreatePlatform();
    }

    void Start()
    {
        m_levelConstructor = LevelConstructor.Instance;
    }

    void OnEnable()
    {
        this.AddGameEventListenerGlobal<CreatePlatformEvent>(OnCreatePlatformEvent);
    }

    void OnDisable()
    {
        this.RemoveGameEventListenerGlobal<CreatePlatformEvent>(OnCreatePlatformEvent);
    }

}
