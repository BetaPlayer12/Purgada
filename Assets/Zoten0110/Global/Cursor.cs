using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour {

    private RectTransform m_rectTransform;

    void Start()
    {
        m_rectTransform = GetComponent<RectTransform>();
    }

	// Update is called once per frame
	void Update () {
        m_rectTransform.position = Input.mousePosition;
    }
}
