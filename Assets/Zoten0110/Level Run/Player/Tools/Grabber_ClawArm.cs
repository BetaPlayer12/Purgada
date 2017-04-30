using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber_ClawArm : MonoBehaviour
{
    [SerializeField]
    private Grabber m_grabber;

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Grabber Hit");
        m_grabber.Retract();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Grabber Hit");
        m_grabber.Retract();
    }

}
