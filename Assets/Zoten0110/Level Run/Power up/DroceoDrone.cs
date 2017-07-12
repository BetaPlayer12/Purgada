using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroceoDrone : IPowerup
{
    private List<Trash> m_scannedTrash = new List<Trash>();

    public override Type type
    {
        get
        {
            return Type.Droceo_Drone;
        }
    }

    private void RemoveNullOnList()
    {
        for (int i = m_scannedTrash.Count - 1; i >= 0; i--)
        {
            if (m_scannedTrash[i] == null)
            {
                m_scannedTrash.RemoveAt(i);
            }
        }
    }

    protected override void PowerupFunction()
    {
        //Either shows the next trash to appear
    }

    private void Update()
    {
        RemoveNullOnList();
        PowerupFunction();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var trash = collision.GetComponent<Trash>();

        if (trash)
        {
            m_scannedTrash.Add(trash);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var trash = collision.GetComponent<Trash>();

        if (trash)
        {
            for (int i = 0; i < m_scannedTrash.Count; i++)
            {
                if (m_scannedTrash[i] == trash)
                {
                    m_scannedTrash.RemoveAt(i);
                    return;
                }
            }
        }
    }
}
