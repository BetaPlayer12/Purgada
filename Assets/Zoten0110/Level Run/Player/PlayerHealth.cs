﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathEvent : GameEvent
{
    public PlayerDeathEvent(GameObject senderObj) : base(senderObj)
    {
        sender = senderObj;
    }
}

public class PlayerDamageEvent : GameEvent
{

    public PlayerDamageEvent(GameObject senderObj) : base(senderObj)
    {
        sender = senderObj;

    }
}

public class PlayerHealth : MonoBehaviour {

    [SerializeField]
    private float m_maxHealth;
    [SerializeField]///Debug;
    private float m_currentHealth;

    private float m_damageReduction;

    private bool m_invulnerable;
    private bool m_dead;

    public float currentHealth { get { return m_currentHealth; } }
    public float currentHealthRatio { get { return m_currentHealth / m_maxHealth; } }

    public void BecomeInvulnerable(bool value) =>
        m_invulnerable = value;

    public void BecomeTemporilyInvulnerable(float duration)
    {
        StopAllCoroutines();
        StartCoroutine(TemporaryInvulnerable(duration));
    }

    private IEnumerator TemporaryInvulnerable(float duration)
    {
        BecomeInvulnerable(true);
        yield return new WaitForSeconds(duration);
        BecomeInvulnerable(false);
    }

    

    public void SetDamageReduction(float damageReduction)
    {
        m_damageReduction = damageReduction;
    }

    public void Damage(float damage)
    {
        if (m_invulnerable)
            return;

        m_currentHealth -= damage - (damage * (m_damageReduction/100) );

        this.RaiseGameEventGlobal(new PlayerDamageEvent(gameObject));

        if (m_currentHealth <= 0f)
        {
            this.RaiseGameEventGlobal(new PlayerDeathEvent(gameObject));
            m_dead = true;
        }
    }

    public void Heal(float heal)
    {
        m_currentHealth += heal;

        if(m_currentHealth > m_maxHealth)
        {
            m_currentHealth = m_maxHealth;
        }
    }



    private void OnTrashMissedEvent(TrashMissedEvent e) =>
        Damage(GlobalGameSettings.Instance.trashMissDamage);


    private void OnPlayerStatusReqestEvent(PlayerStatusReqestEvent e)
    {
        e.isInvulnerable = m_invulnerable;
        e.isDead = m_dead;
    }

    // Use this for initialization
    void Start ()
    {
        m_currentHealth = m_maxHealth;
        m_dead = false;
    }

    void OnEnable()
    {
        this.AddGameEventListenerGlobal<TrashMissedEvent>(OnTrashMissedEvent);
        this.AddGameEventListenerGlobal<PlayerStatusReqestEvent>(OnPlayerStatusReqestEvent);
       
    }

    void OnDisable()
    {
        this.RemoveGameEventListenerGlobal<TrashMissedEvent>(OnTrashMissedEvent);
        this.RemoveGameEventListenerGlobal<PlayerStatusReqestEvent>(OnPlayerStatusReqestEvent);

    }
}
