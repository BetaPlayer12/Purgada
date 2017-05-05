using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceToMovement : MonoBehaviour
{

    public bool m_is2D;
    private Rigidbody m_rigidbody;
    private Rigidbody2D m_rigidbody2D;

    // Use this for initialization
    void Start()
    {
        if (m_is2D)
        {

            m_rigidbody2D = GetComponent<Rigidbody2D>();
        }
        else
        {
            m_rigidbody = GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_is2D)
        {
            var rotation = Quaternion.LookRotation(m_rigidbody2D.velocity);
            rotation.z = -rotation.x;
            rotation.x = 0;
            rotation.y = 0;

            transform.rotation = rotation;
        }
        else
        {
            var rotation = Quaternion.LookRotation(m_rigidbody.velocity);

            transform.rotation = rotation;
        }


    }
}
