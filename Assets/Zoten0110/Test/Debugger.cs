using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DebugObject
{
    void OnDebug();
}

public class Debugger : MonoBehaviour
{

    public MonoBehaviour[] m_debugObjects;

    public bool inDebugMode;

    // Use this for initialization
    void Start()
    {
        if (inDebugMode)
        {
            foreach(MonoBehaviour debugObject in m_debugObjects)
            {
                var components = debugObject.GetComponents<DebugObject>();
                foreach (DebugObject component in components)
                {
                    component.OnDebug();
                }
            }
        }
    }

}
