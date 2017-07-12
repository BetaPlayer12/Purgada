using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbRepair : IPowerup
{
    [SerializeField]
    private OrbRepair_Creature m_creature;
    private bool m_hasDesignatedTarget; 
    private List<Obstacle> m_obstacleList = new List<Obstacle>();

    public Rigidbody2D m_rig;

    public override Type type
    {
        get
        {
            return Type.Orb_Repair;
        }
    }

    protected override void PowerupFunction()
    {
        if (m_creature.hasTarget)
        {
            m_hasDesignatedTarget = true;
        }
        else
        {
            if (m_hasDesignatedTarget)
            {
                m_hasDesignatedTarget = false;
                RemoveNullOnList();
            }
            if (m_obstacleList.Count != 0)
            {
                if (m_obstacleList[0] != null)
                {
                    m_hasDesignatedTarget = true;
                    m_creature.SetTarget(m_obstacleList[0]);
                }
            }
        }
    }

    private void RemoveNullOnList()
    {
        for (int i = m_obstacleList.Count-1; i >= 0; i--)
        {
           if( m_obstacleList[i] == null){
                m_obstacleList.RemoveAt(i);
            }
        }
    }


    private void Update()
    {
        PowerupFunction();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Sensor Caught");
        var obstacle = collision.GetComponent<Obstacle>();
        if (obstacle)
        {
            m_obstacleList.Add(obstacle);
        }
    }


}
