using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUIManager : MonoBehaviour {

    private Animator m_animator;

    public void ToLevelRun(bool value)
    {
        m_animator.SetBool("Level Run", value);
    }

	// Use this for initialization
	void Start () {
        m_animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
