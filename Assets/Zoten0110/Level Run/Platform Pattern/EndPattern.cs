using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPattern : MonoBehaviour
{
    private RectTransform m_parent;
    private float m_referenceResolutionX;
    private float m_offset;
    private float m_relativeXPosition;

    void Start()
    {
        m_parent = transform.parent.GetComponent<RectTransform>(); ;
        m_referenceResolutionX = GetComponentInParent<CanvasScaler>().referenceResolution.x;
        m_offset = GetComponent<RectTransform>().localPosition.x;
    }

    void Update()
    {
        m_relativeXPosition = m_parent.localPosition.x + m_offset;

        if (m_relativeXPosition <= -m_referenceResolutionX)
        {
            Destroy(m_parent.gameObject);
        }
    }
}
