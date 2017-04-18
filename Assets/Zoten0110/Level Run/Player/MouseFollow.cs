using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    public Vector2 m_offset;
    public float m_angleOffset;
    // Update is called once per frame
    void Update()
    {
        var mouse = Input.mousePosition;
        var screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        var offset = new Vector2(mouse.x - screenPoint.x + m_offset.x, mouse.y - screenPoint.y + m_offset.y);
        var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + m_angleOffset);
    }
}
