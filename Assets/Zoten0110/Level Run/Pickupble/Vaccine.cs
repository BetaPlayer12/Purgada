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

    protected override void OnPickup(GameObject GO)
    {
        throw new NotImplementedException();
    }
}
