using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_Pickup : IPickup {

    [SerializeField]
    private IPowerup.Type m_type;

    protected override void OnPickup(GameObject GO)
    {
        this.RaiseGameEventGlobal<ActivatePowerupEvent>(new ActivatePowerupEvent(gameObject, m_type));
    }
}
