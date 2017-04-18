using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlatformEvent : GameEvent
{
}

public class Platform_Generator : MonoBehaviour {

    [SerializeField]
    private Transform m_parent;
    [SerializeField]
    private GameObject m_platform;
    public float m_spawnScale;

    private void CreatePlatform()
    {
        var instance = Instantiate(m_platform, m_parent) as GameObject;
        var instanceTransform = instance.transform;
        instanceTransform.localPosition = Vector3.zero;
        instanceTransform.localScale = Vector3.one * m_spawnScale;
    }

    private void OnCreatePlatformEvent(CreatePlatformEvent e)
    {
        CreatePlatform();
    }

    void Start()
    {
        CreatePlatform();
    }

    void OnEnable()
    {
        this.AddEventListenerGlobal<CreatePlatformEvent>(OnCreatePlatformEvent);
    }

    void OnDisable()
    {
        this.RemoveEventListenerGlobal<CreatePlatformEvent>(OnCreatePlatformEvent);
    }

}
