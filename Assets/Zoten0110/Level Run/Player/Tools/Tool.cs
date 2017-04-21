using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool : MonoBehaviour
{

    private ToolController m_toolController;

    private bool m_isJammed;


    public abstract void Shoot();

    private void OverrideShoot()
    {
        m_toolController.Shoot = Shoot;
    }


    public void Select()
    {
        OverrideShoot();
    }

    // Use this for initialization
    void Awake()
    {
        m_toolController = GetComponentInParent<ToolController>();
    }
}
