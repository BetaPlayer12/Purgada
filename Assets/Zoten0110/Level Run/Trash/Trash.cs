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
    private Sprite m_sprite;
    private Type m_trashType;

    public void SetInfo(Sprite newSprite, Type type)
    {
        m_sprite = newSprite;
        m_trashType = type;
    }

    void Start()
    {
        m_rigidBody.simulated = false;
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

    void OnTriggerStay2D(Collider2D other)
    {
        m_rigidBody.simulated = true;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
       // m_rigidBody.simulated = false;
    }
}
