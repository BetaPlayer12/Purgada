using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Follows an Object where ever it goes
/// </summary>
public class ObjectFollow : MonoBehaviour
{

    [SerializeField]
    [Tooltip("What Object to follow")]
    private Transform m_target;
    [SerializeField]
    [Tooltip("Current position will be use as offset")]
    private bool m_useCurrentAsOffset;
    [SerializeField]
    [Tooltip("Offset position of the object as it follows the target")]
    private Vector3 m_offset;
    [SerializeField]
    [Tooltip("How fast will the object follow")]
    private float m_smoothing = 100f;
    [SerializeField]
    [Tooltip("On Start, the object will be on its position already")]
    private bool m_startAtOffset;

    public bool OnTarget { get { return Vector3.Distance(transform.position, m_target.position) <= 1f; } }

    public void Follow(Transform target)
    {
        m_target = target;
    }

    void Start()
    {
        if (m_useCurrentAsOffset)
        {
            m_offset = transform.position - m_target.position;
        }
        else if (m_startAtOffset)
        {

            transform.position = m_offset + m_target.position;
        }
    }

    void FixedUpdate()
    {
        if (m_target)
        {
            transform.position = Vector3.Lerp(transform.position, m_target.position + m_offset, m_smoothing * Time.deltaTime);
        }
    }
}
