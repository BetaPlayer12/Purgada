using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TimerViewer : MonoBehaviour {

    [SerializeField]
    private Timer m_attachedTimer;
    [SerializeField]
    private bool m_inverted;
    private float m_initialTime;
    private Image m_image;

    private float ratio { get { return m_attachedTimer.value / m_initialTime; } }

    public void AttachTimer(Timer timer)
    {
        m_attachedTimer = timer;
        m_initialTime = timer.value;
    }

    void Start()
    {
        m_image = GetComponent<Image>();
        if(m_attachedTimer != null)
        {
            m_initialTime = m_attachedTimer.value;
        }
    }

    void Update()
    {
        m_image.fillAmount = m_inverted? 1-ratio : ratio;
    }
}
