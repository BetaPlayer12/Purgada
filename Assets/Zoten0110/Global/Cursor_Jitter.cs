using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor_Jitter : MonoBehaviour {

    private RectTransform m_rectTransform;
    [SerializeField]
    private Vector2 m_jitterRange;
    [SerializeField]
    private float m_speed;
    private bool m_isJittering;
    private bool m_istravelling;
    private bool m_isCentered;

    private Vector3 m_jitterDestination;
    private Vector3 m_travelDirection;

    public void SetJitter(Vector2 jitterRange, float speed)
    {
        m_jitterRange = jitterRange;
        m_speed = speed;
    }

    public void Jitter(bool value) =>
        m_isJittering = value;

    void Start()
    {
        m_rectTransform = GetComponent<RectTransform>();
        m_isCentered = true;
    }

    // Update is called once per frame
    void Update () {
        if (m_isJittering)
        {
            if (m_istravelling)
            {
                m_rectTransform.localPosition += m_travelDirection * m_speed * Time.deltaTime;
                if (Vector3.Distance(m_rectTransform.localPosition, m_jitterDestination) <= 1f)
                {
                    m_istravelling = false;
                }
            }
            else
            {
                m_jitterDestination = new Vector3(Random.Range(-m_jitterRange.x, m_jitterRange.x),Random.Range(-m_jitterRange.y, m_jitterRange.y), 0);
                m_travelDirection = (m_jitterDestination - m_rectTransform.localPosition).normalized;
                m_istravelling = true;
            }
            m_isCentered = false;
        }
        else if(m_isCentered == false)
        {
            m_istravelling = false;
            m_travelDirection = (Vector3.zero - m_rectTransform.localPosition).normalized;
            m_rectTransform.localPosition += m_travelDirection * m_speed * Time.deltaTime;

            if (Vector3.Distance(m_rectTransform.localPosition, Vector3.zero) <= 1f)
            {
                m_isCentered = true;
                m_rectTransform.localPosition = Vector3.zero;
            }
        }
    }
}
