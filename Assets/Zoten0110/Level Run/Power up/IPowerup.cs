using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IPowerup : MonoBehaviour {

    [SerializeField]
    protected GameObject m_parentObject;
    private float m_duration;

    protected void UpdateDuration(float duration)
    {
        m_duration = duration;
    }

    protected abstract void PowerupFunction();

    protected IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(m_duration);
        Destroy(m_parentObject);
    }
}
