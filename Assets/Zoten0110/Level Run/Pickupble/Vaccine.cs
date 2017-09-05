using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnVaccineEffectEvent : GameEvent
{
    public OnVaccineEffectEvent(GameObject sender) : base(sender)
    {
    }
}

public class Vaccine : IPickup {

    [SerializeField]
    private float m_duration;

    protected override void OnPickup(GameObject GO)
    {
        var playerHealth = GO.GetComponent<PlayerHealth>();
        if (playerHealth)
        {
            playerHealth.BecomeTemporilyInvulnerable(m_duration);
            this.RaiseGameEventGlobal<ShowCommentEvent>(new ShowCommentEvent(gameObject, "You feel Invulnerable"));
            TimerFactory.Instance.Create("Vaccine", m_duration);
        }
    }
}
