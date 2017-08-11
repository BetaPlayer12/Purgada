using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiseaseEndEvent : GameEvent
{
    public DiseaseEndEvent(GameObject senderObj) : base(senderObj)
    {
        sender = senderObj;
    }
}

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
        this.RaiseGameEventGlobal(new DiseaseEndEvent(gameObject));
    }

    protected abstract void DiseaseStart();
    protected abstract void DiseaseEnd();

    public void Inflict()
    {
        if (!m_isActive)
        {
            StartCoroutine(DiseaseEffect());
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(DiseaseEffect());
        }

    }
}
