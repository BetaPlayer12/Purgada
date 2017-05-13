using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TokenTypes
{
    Miracle_Drug,
    Sponsorship,
    Booster_Shot
}

public abstract class IToken : MonoBehaviour
{
    protected bool m_isActive;

    public abstract TokenTypes type {get;}

    public virtual void MakeActive()
    {
        m_isActive = true;
    }

}

