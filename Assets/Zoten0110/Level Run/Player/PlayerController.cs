using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Contains all the controls for the player object
/// </summary>
public class PlayerController : MonoBehaviour
{

    private Rigidbody2D m_rigidbody;

    [Header("Jump Properties")]
    [SerializeField]
    private float m_jumpForce;

    private bool m_isJumping;
    private bool m_canDrop;

    private bool jumpInput { get { return Input.GetAxis("Jump") > 0; } }

    // Use this for initialization
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Input Checks
        {
            if (jumpInput && !m_isJumping)
            {
                m_rigidbody.AddForce(Vector3.up * m_jumpForce, ForceMode2D.Impulse);
                m_isJumping = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        m_isJumping = false;
        m_canDrop = other.gameObject.GetComponent<PlatformEffector2D>() != null ? true : false;
    }

    void OnCollisionExit2D(Collider2D other)
    {
        m_isJumping = true;
    }
}
