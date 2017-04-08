using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMovement : MonoBehaviour {

    public Vector3 m_move;
	
	// Update is called once per frame
	void Update () {
        transform.position += m_move * Time.deltaTime;
	}
}
