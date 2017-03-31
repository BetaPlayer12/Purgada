using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectViewer : MonoBehaviour
{


    [Header("Object Settings")]
    [SerializeField]
    protected GameObject m_instancePrefab;
    [SerializeField]
    protected Transform m_parentTransform;
    [SerializeField]
    protected Quaternion m_localRotation;
    [SerializeField]
    protected Vector3 m_localScale = Vector3.one;

    [Header("Viewing Settings")]
    [SerializeField]
    protected int m_xPos;
    [SerializeField]
    protected int m_yPos;
    [SerializeField]
    protected int m_xOffset;
    [SerializeField]
    protected int m_yOffset;
    [SerializeField][Tooltip("If maxRow is on the number of perRow will become maxRow")]
    protected int m_perRow = 1;
    [SerializeField]
    protected bool m_maxRow = false;

    private int m_currentXPos;
    private int m_currentYPos;
    protected List<GameObject> m_GOList;

    protected int currentXpos { get { return m_currentXPos; } }
    protected int currentYpos { get { return m_currentYPos; } }
    protected virtual int objectCount { get { return 1; } }
    public List<GameObject> GOList { get { return m_GOList; } }

    void Awake()
    {
        m_currentXPos = m_xPos;
        m_currentYPos = m_yPos;
        m_GOList = new List<GameObject>();

        if (m_maxRow && m_perRow > 0)
            m_perRow = Mathf.CeilToInt((float)objectCount / (float)m_perRow);

        OnAwakeModule();
    }

    protected GameObject CreateObject(string name = null)
    {
        var parentObject = m_parentTransform != null ? m_parentTransform : transform;

        var newObject = Instantiate(m_instancePrefab, parentObject) as GameObject;

        newObject.transform.localPosition = new Vector2(m_currentXPos, m_currentYPos);
        newObject.transform.localRotation = m_localRotation;
        newObject.transform.localScale = m_localScale;

        if (name != null)
            newObject.name = name;


        m_currentXPos += m_xOffset;

        if (m_perRow != 0)
            if (m_perRow == 1 || (m_currentXPos - m_xPos) / m_xOffset == m_perRow)
            {
                m_currentXPos = m_xPos;
                m_currentYPos += m_yOffset;
            }

        m_GOList.Add(newObject);
        return newObject;
    }

    protected virtual void OnAwakeModule() { }
}
