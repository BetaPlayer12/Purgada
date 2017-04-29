using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    private RectTransform m_rectTransform;
    [SerializeField]
    private Cursor_Jitter m_jitterScript;
    [SerializeField]//Debug
    private bool m_isJittering;

    public void Jitter(bool value) =>
        m_jitterScript.Jitter(m_isJittering = value);

    void Start()
    {
        m_rectTransform = GetComponent<RectTransform>();
        m_jitterScript.Jitter(m_isJittering);
    }

    // Update is called once per frame
    void Update()
    {
        m_rectTransform.position = Input.mousePosition;
    }
}
