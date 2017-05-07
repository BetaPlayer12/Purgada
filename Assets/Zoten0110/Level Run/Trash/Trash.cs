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
    private Rigidbody2D m_rigidBody;
    [SerializeField]
    private Type m_trashType;
    private int m_ID;

    public Type trashType { get { return m_trashType; } }
    public int ID { get { return m_ID; } }

    public void SetInfo(Type type, int trashID)
    {
        m_trashType = type;
        m_ID = trashID;
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
