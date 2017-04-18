using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeginPattern : MonoBehaviour
{
    private float m_referenceResolutionX;
    private bool m_hasRaisedEvent;

    void Start()
    {
        m_referenceResolutionX = GetComponentInParent<CanvasScaler>().referenceResolution.x;
        m_hasRaisedEvent = false;
    }

    void Update()
    {
        if (transform.localPosition.x <= -m_referenceResolutionX && !m_hasRaisedEvent)
        {
            this.RaiseEventGlobal<CreatePlatformEvent>(new CreatePlatformEvent { sender = gameObject });
            m_hasRaisedEvent = true;
        }
    }
}
