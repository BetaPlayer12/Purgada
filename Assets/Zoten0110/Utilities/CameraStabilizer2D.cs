using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraStabilizer2D : MonoBehaviour {


	// Use this for initialization
	void Start () {
        var camera = GetComponent<Camera>();
        camera.orthographic = true;
        camera.orthographicSize = Screen.height / 2;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
