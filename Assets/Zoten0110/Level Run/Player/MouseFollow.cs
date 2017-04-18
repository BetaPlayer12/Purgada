using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    public Vector2 m_offset;

    private RectTransform m_rectTransform;
    private Camera m_mainCamera;

    void Start()
    {
        m_rectTransform = transform.parent.GetComponent<RectTransform>();
        m_mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        var mousPosition = Input.mousePosition;

        // Get Angle in Radians
        float AngleRad = Mathf.Atan2(mousPosition.y - m_rectTransform.localPosition.y + m_offset.y, mousPosition.x - m_rectTransform.localPosition.x + m_offset.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object
        this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
    }
}
