using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTool : MonoBehaviour {

    public delegate void ShootFunc();
    public ShootFunc Shoot;

    [SerializeField]
    private Tool[] m_tools;
    private int m_toolIndex;



    private bool isShooting { get { return Input.GetAxis("MouseX") > 0; } }
    private bool isSwapingTool { get { return Input.GetAxis("Swap Tool") != 0; } }
	
    private void SwapTool()
    {
        if (isSwapingTool)
        {
            m_toolIndex += (int)Input.GetAxis("Swap Tool");
            m_tools[m_toolIndex].Select();
        }
    }

	// Update is called once per frame
	void Update () {

        if (isShooting)
        {
            Shoot?.Invoke();
        }

	}
}
