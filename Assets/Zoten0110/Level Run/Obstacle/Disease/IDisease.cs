using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IDisease : MonoBehaviour
{
    [SerializeField]
    protected float m_duration;
    private bool m_isActive;

    private IEnumerator DiseaseEffect()
    {
        DiseaseStart();
        m_isActive = true;
        yield return new WaitForSeconds(m_duration);
        m_isActive = false;
        DiseaseEnd();
    }

    protected abstract void DiseaseStart();
    protected abstract void DiseaseEnd();

    public void Inflict()
    {
        if (!m_isActive)
        {
            StartCoroutine(DiseaseEffect());
        }
    }
}
