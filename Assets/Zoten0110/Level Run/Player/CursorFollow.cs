using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorFollow : MonoBehaviour, DebugObject
{
    public RectTransform m_cursor;
    public Vector2 m_offset;
    public float m_angleOffset;

    private bool m_isActive;

    private void OnLevelStartEvent(LevelStartEvent e)
    {
        m_isActive = true;
    }

    void OnEnable()
    {
        this.AddGameEventListenerGlobal<LevelStartEvent>(OnLevelStartEvent);
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_isActive)
            return;

        var mouse = m_cursor.position;
        var screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        var offset = new Vector2(mouse.x - screenPoint.x + m_offset.x, mouse.y - screenPoint.y + m_offset.y);
        var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + m_angleOffset);
    }

    void OnDisable()
    {
        this.RemoveGameEventListenerGlobal<LevelStartEvent>(OnLevelStartEvent);
    }

    void DebugObject.OnDebug()
    {
        m_isActive = true;
    }
}
