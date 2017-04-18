using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool : MonoBehaviour {

    private bool m_isJammed;

    public abstract void Select();

	// Use this for initialization
	void Start () {
		
	}
}
