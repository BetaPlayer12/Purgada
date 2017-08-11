using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IEncounter : MonoBehaviour {

    [SerializeField]
    private float m_yThrowForce;
    [SerializeField]
    private float m_maxXThrowForce;
    [SerializeField]
    private float m_maxWaitTime;
    [SerializeField]
    private Vector3 m_instanceOffset;

    protected abstract GameObject GetObject();

    public void StartThrowObject()
    {
        StartCoroutine(WaitTime());
    }

    private IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(m_maxWaitTime);
        ThrowObject();
    }

    public void ThrowObject()
    {
        var throwable = Instantiate(GetObject(), m_instanceOffset + transform.position, Quaternion.identity);
        throwable.transform.parent = transform.parent;
        var throwableRigidbody = throwable.GetComponent<Rigidbody2D>();

        var directionOfThrow = Random.Range(0, 100) > 50 ? 1 : -1;

        throwableRigidbody.AddForce(new Vector3(Random.Range(0, m_maxXThrowForce) * directionOfThrow, m_yThrowForce), ForceMode2D.Impulse);
        throwableRigidbody.AddTorque(Random.Range(0f, 100f) * directionOfThrow);
    }
}
