using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPattern : MonoBehaviour
{
    private float m_screenWidth;


    void Start()
    {
        m_screenWidth = Screen.width;
    }

    void Update()
    {
        if (transform.localPosition.x <= -m_screenWidth)
        {
            this.RaiseEventGlobal<CreatePlatformEvent>(new CreatePlatformEvent { sender = gameObject });
            Destroy(gameObject);
        }
    }
}
