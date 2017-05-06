
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    [SerializeField]
    private float m_value;
    [SerializeField]
    private bool m_deactivateOnly;

    public float value { get { return m_value; } }
    public bool deactivateOnly { get { return m_deactivateOnly; } }

    public void SetTimer(float value)
    {
        m_value = value;
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        m_value -= Time.deltaTime;
        if(m_value<0f)
        {
            if(m_deactivateOnly)
            {
                gameObject.SetActive(false);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
