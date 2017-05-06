using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool : MonoBehaviour
{

    private ToolController m_toolController;
   
    protected bool m_lockInput;


    private bool AllClear()=>
       !m_lockInput;

    public void Shoot()
    {
        if (m_lockInput)
            return;

        Activate();
    }

    public abstract void Activate();

    private void OverrideShoot()
    {
        Debug.Log(GetType().ToString() + " is Selected");
        m_toolController.Shoot = Shoot;
        m_toolController.AllClear = AllClear;
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
