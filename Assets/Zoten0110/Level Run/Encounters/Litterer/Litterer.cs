using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Litterer : IEncounter
{
    protected override GameObject GetObject()
    {
       return LevelConstructor.Instance.GetTrash();
    }
}
