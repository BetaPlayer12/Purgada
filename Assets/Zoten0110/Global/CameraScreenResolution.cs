using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Only works with Orthographic Camera
/// </summary>
[RequireComponent(typeof(Camera))]
public class CameraScreenResolution : MonoBehaviour
{
    public enum Focus
    {
        Top = -1,
        Center,
        Bottom
    }

    public bool m_maintainWidth;
    public Focus m_heightFocus;
    public bool m_updates;

    private Camera m_camera;
    private static Vector2 m_referenceResolution = new Vector2(1920f, 1080f);
    private static float m_referenceAspect;

    private Vector3 m_initialCameraPosition;
    private float m_defaultHeight;
    private float m_defaultWidth;

    public float width { get { return m_camera.orthographicSize * m_camera.aspect; } }


    private void Adjust()
    {
        if (m_maintainWidth)
        {
            m_camera.orthographicSize = m_defaultWidth / m_camera.aspect;
            m_camera.transform.position = new Vector3(m_initialCameraPosition.x, m_initialCameraPosition.y + (int)m_heightFocus * (m_defaultHeight - m_camera.orthographicSize), m_initialCameraPosition.z);
        }
        else
        {
            Camera.main.transform.position = new Vector3(m_initialCameraPosition.x + (int)m_heightFocus * (m_defaultWidth - Camera.main.orthographicSize * Camera.main.aspect), m_initialCameraPosition.y, m_initialCameraPosition.z);
        }
    }

	// Use this for initialization
	void Awake () {

        m_camera = GetComponent<Camera>();
        m_referenceAspect = m_referenceResolution.x / m_referenceResolution.y;

        m_initialCameraPosition = m_camera.transform.position;
        m_defaultHeight = m_camera.orthographicSize;
        m_defaultWidth = m_defaultHeight * m_referenceAspect;
        

        if (!m_updates)
        {
            Destroy(this);
        }
    }
	
	// Update is called once per frame
	void Update () {
        Adjust();
    }
}
