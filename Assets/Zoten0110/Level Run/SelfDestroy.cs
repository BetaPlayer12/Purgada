using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour {


    [SerializeField]
    private float m_lifetime;

    private IEnumerator Kill()
    {
        yield return new WaitForSeconds(m_lifetime);
        Destroy(gameObject);
    }

	// Use this for initialization
	void Start () {
        StartCoroutine(Kill());
	}
}
