using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoilCannon : Tool {

    [SerializeField]
    private GameObject m_projectile;
    [SerializeField]
    private Transform m_barrelEnd;
    [SerializeField][Tooltip("How many projectiles fired in every second")]
    private float m_fireRate;
    [SerializeField]
    private float m_force;

    private float m_timeInterval; //calculation derived from fireRate

    private IEnumerator CoolDown()
    {
        yield return new WaitForSecondsRealtime(m_timeInterval);
        m_lockInput = false;
    }

    private void CreateProjectile()
    {
        var projectile = Instantiate(m_projectile, m_barrelEnd.position, transform.parent.rotation);
        projectile.GetComponent<Rigidbody2D>().AddForce(projectile.transform.right * m_force, ForceMode2D.Impulse);
    }

    public override void Activate()
    {
        Debug.Log("SoilCannon Shot");
        m_lockInput = true;
        CreateProjectile();
        StartCoroutine(CoolDown());
    }

    // Use this for initialization
    void Start () {
        m_timeInterval = 1f/ m_fireRate;
    }
	
}
