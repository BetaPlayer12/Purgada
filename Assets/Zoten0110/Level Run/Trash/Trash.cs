using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashMissedEvent : GameEvent
{
    public TrashMissedEvent(GameObject senderObj) : base(senderObj)
    {
        sender = senderObj;
    }
}

public class Trash : MonoBehaviour {

    public enum Type
    {
        Organic,
        Toxic,
        Recyclable
    }

    [SerializeField]
    private Sprite m_sprite;
    private Type m_trashType;

    public void SetInfo(Sprite newSprite, Type type)
    {
        m_sprite = newSprite;
        m_trashType = type;
    }

	// Update is called once per frame
	void Update () {
        var screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.x <= -50)
        {
            this.RaiseEventGlobal(new TrashMissedEvent(gameObject));
            Destroy(gameObject);
        }
    }
}
