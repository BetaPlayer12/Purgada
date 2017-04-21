using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathEvent : GameEvent
{
    public PlayerDeathEvent(GameObject senderObj) : base(senderObj)
    {
        sender = senderObj;
    }
}

public class PlayerHealth : MonoBehaviour {

    [SerializeField]
    private float m_maxHealth;
    [SerializeField]///Debug;
    private float m_currentHealth;

    private bool m_dead;

	// Use this for initialization
	void Start () {
        m_currentHealth = m_maxHealth;
        m_dead = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!m_dead)
        {
            if (m_currentHealth <= 0f)
            {
                this.RaiseEventGlobal(new PlayerDeathEvent(gameObject));
                m_dead = true;
            }
        }
	}
}
