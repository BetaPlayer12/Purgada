using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPattern : MonoBehaviour
{
    private float m_referenceResolutionX;

    void Start()
    {
        m_referenceResolutionX = 0;
    }

    void Update()
    {
        var screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        if (screenPosition.x <= -m_referenceResolutionX)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
