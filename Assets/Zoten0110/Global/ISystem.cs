using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISystem : MonoBehaviour
{
    [SerializeField]
    protected string m_systemName;
    [SerializeField]
    protected string m_systemDescription;
    [SerializeField]
    protected bool m_isEnable = true;

    public bool isEnable { get { return m_isEnable; } }

    public bool IsEqual(System.Type type, string name) =>
     (type == GetType() && name == m_systemName);

    public ISystem GetSystem()
    {
        if (m_isEnable)
        {
            return this;
        }
        else
        {
            Debug.Log(gameObject + " " + GetType().ToString()+ " is inactive");
            return null;
        }

    }

}