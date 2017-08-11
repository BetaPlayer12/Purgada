using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber_ClawArm : MonoBehaviour
{
    [SerializeField]
    private Grabber m_grabber;
    [SerializeField]
    private Collider2D m_collider;
    private Trash m_trash;

    [SerializeField]
    private SpriteRenderer m_renderer;
    [SerializeField]
    private Sprite m_spriteMain;
    [SerializeField]
    private Sprite m_spriteOverride;


    public Trash heldTrash { get { return m_trash; } }
    public bool isEmptyHanded { get { return m_trash == null; } }

    public void Enable(bool value)
    {
        m_collider.enabled = value;
    }

    public void OverrideSprite()
    {
        m_renderer.sprite = m_spriteMain;
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Grabber Hit");
        m_grabber.Retract();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Sensors")
        {
            Debug.Log("Grabber Hits " + other.gameObject.name);
            m_renderer.sprite = m_spriteOverride;
            m_grabber.Retract();
            Debug.Log(other.gameObject.layer + " " + LayerMask.NameToLayer("Trash"));
            if (other.gameObject.layer == LayerMask.NameToLayer("Trash"))
            {
                
                var trash = other.gameObject;
                m_trash = trash.GetComponentInParent<Trash>();
                Debug.Log("Grabber Grabs " + m_trash.transform.parent);

                if (m_trash)
                {
                    other.isTrigger = true;
                    m_trash.transform.parent = transform;
                    m_trash.GetComponent<Rigidbody2D>().simulated = false;
                }
            }
        }
    }

}
