using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbRepair_Creature : MonoBehaviour
{
    private Obstacle m_target;
    [SerializeField]
    private float m_speed;
    [SerializeField]
    private Vector3 m_standbyPosition;

    public bool hasTarget { get { return (m_target != null); } }

    public void SetTarget(Obstacle target)
    {
        m_target = target;
    }

    private void Update()
    {
        if(m_target != null)
        {
            
            var direction = m_target.transform.position.x - transform.position.x;
            transform.position += Vector3.right * direction * m_speed * Time.deltaTime;

        }
        else
        {
            var direction = m_standbyPosition.x - transform.position.x;
            transform.localPosition += Vector3.right * direction * m_speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var obstacle = collision.GetComponent<Obstacle>();
        if (obstacle == m_target)
        {
            Destroy(obstacle.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var obstacle = collision.GetComponent<Obstacle>();
        if (obstacle == m_target)
        {
            Destroy(obstacle.gameObject);
        }
    }
}
