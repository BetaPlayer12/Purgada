﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginBackground : MonoBehaviour {
    private float m_referenceResolutionX;
    private bool m_hasRaisedEvent;
    private bool m_enableCall;

    void Start()
    {
        m_referenceResolutionX = 0;
        m_hasRaisedEvent = false;
        m_enableCall = GlobalGameSettings.Instance.enablePlatformGenerator;
    }

    void Update()
    {
        if (m_enableCall)
        {
            var screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            if (screenPosition.x <= m_referenceResolutionX && !m_hasRaisedEvent)
            {
                this.RaiseGameEventGlobal(new CreateBackgroundEvent(gameObject));
                m_hasRaisedEvent = true;
            }

        }
    }
}
