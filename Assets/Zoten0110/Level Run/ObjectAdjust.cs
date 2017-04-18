using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAdjust : MonoBehaviour
{

    public enum ScreenAnchor
    {
        Top_Left,
        Top_Middle,
        Top_Right,
        Center_Left,
        Center_Middle,
        Center_Right,
        Bottom_Left,
        Bottom_Middle,
        Bottom_Right,
    }

    [Header("Position Settings")]
    public Vector2 m_offset;
    public ScreenAnchor m_anchor;
    public bool m_local;
    public bool m_updates;

    private static Vector2 m_resolutionReference = new Vector2(1920f, 1080f);
    private Vector2 m_defaultOffset;


    private void RecalculateOffset()
    {
        var cameraScreenRes = FindObjectOfType<CameraScreenResolution>();

        Vector2 ratio = new Vector2(m_defaultOffset.x / m_resolutionReference.x, m_defaultOffset.y / m_resolutionReference.y);
        m_offset.Set(ratio.x * Screen.width, ratio.y * Screen.height);
    }

    private void Adjust()
    {
        Vector3 newPosition = Vector3.zero;
        Vector3 offset = new Vector3(m_offset.x, m_offset.y, 0f);
        switch (m_anchor)
        {
            case ScreenAnchor.Top_Left:
                newPosition = Camera.main.ScreenToWorldPoint((Vector3.up * Screen.height) + offset);
                break;
            case ScreenAnchor.Top_Middle:
                newPosition = Camera.main.ScreenToWorldPoint((Vector3.up * Screen.height) + offset + (Vector3.right * Screen.width / 2));
                break;
            case ScreenAnchor.Top_Right:
                newPosition = Camera.main.ScreenToWorldPoint((Vector3.up * Screen.height) + offset + (Vector3.right * Screen.width));
                break;
            case ScreenAnchor.Bottom_Left:
                newPosition = Camera.main.ScreenToWorldPoint(m_offset);
                break;
            case ScreenAnchor.Bottom_Middle:
                newPosition = Camera.main.ScreenToWorldPoint(offset + (Vector3.right * Screen.width / 2));
                break;
            case ScreenAnchor.Bottom_Right:
                newPosition = Camera.main.ScreenToWorldPoint(offset + (Vector3.right * Screen.width));
                break;
            case ScreenAnchor.Center_Left:
                newPosition = Camera.main.ScreenToWorldPoint(offset + (Vector3.up * Screen.height / 2));
                break;
            case ScreenAnchor.Center_Middle:
                newPosition = Camera.main.ScreenToWorldPoint(offset + (Vector3.up * Screen.height / 2) + (Vector3.right * Screen.width / 2));
                break;
            case ScreenAnchor.Center_Right:
                newPosition = Camera.main.ScreenToWorldPoint((Vector3.right * Screen.width) + offset + (Vector3.up * Screen.height / 2));
                break;
        }
        newPosition.z = 1;
        if (m_local)
        {
            transform.position = newPosition;
        }
        else
        {
            transform.position = newPosition;
        }
    }

    void Start()
    {
        m_defaultOffset = m_offset;
    }

    void Update()
    {
        RecalculateOffset();
        Adjust();
        if (!m_updates)
        {
            Destroy(this);
        }
    }
}
