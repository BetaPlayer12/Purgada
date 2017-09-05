using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbRepair_Creature : MonoBehaviour
{
    #region Old Code
    //private Obstacle m_target;
    //[SerializeField]
    //private float m_speed;
    //[SerializeField]
    //private Vector3 m_standbyPosition;

    //public bool hasTarget { get { return (m_target != null); } }

    //public void SetTarget(Obstacle target)
    //{
    //    m_target = target;
    //}

    //private void Update()
    //{
    //    if(m_target != null)
    //    {

    //        var direction = m_target.transform.position.x - transform.position.x;
    //        transform.position += Vector3.right * direction * m_speed * Time.deltaTime;

    //    }
    //    else
    //    {
    //        var direction = m_standbyPosition.x - transform.position.x;
    //        transform.localPosition += Vector3.right * direction * m_speed * Time.deltaTime;
    //    }
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    var obstacle = collision.GetComponent<Obstacle>();
    //    if (obstacle == m_target)
    //    {
    //        Destroy(obstacle.gameObject);
    //    }
    //}

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    var obstacle = collision.GetComponent<Obstacle>();
    //    if (obstacle == m_target)
    //    {
    //        Destroy(obstacle.gameObject);
    //    }
    //}
    #endregion

    [SerializeField]
    private GameObject m_spawnCreature;
    private Animator m_anim;
    private Transform m_target;
    [SerializeField]
    private ObjectFollow m_objectFollow;

    private Obstacle currentTarget;

    public void SetTarget(Transform target,Obstacle oTarget)
    {
        m_target = target;
        currentTarget = oTarget;
        m_objectFollow.Follow(target);
    }

    public void Spawn(Obstacle target)
    {
        Debug.Log(target);
        currentTarget = target;
        m_anim.SetTrigger("Spawn");
    }

    public void SpawnCreature()
    {
        var instance = Instantiate(m_spawnCreature, transform.position, Quaternion.identity);
        var creature = instance.GetComponentInChildren<OrbRepair_Creature>();
        creature.SetTarget(currentTarget.transform, currentTarget);
        creature.StartDelayDestroy();
    }

    public void DestoryTarget()
    {
        if (currentTarget)
        {
            Destroy(currentTarget.gameObject);
        }
    }

    public void Destroy()
    {
        StopAllCoroutines();
        Destroy(transform.root.gameObject);
    }

    private void Start()
    {
        m_anim = GetComponent<Animator>();
    }

    public void StartDelayDestroy()
    {
        StartCoroutine(ConditionalDelayDestroy());
    }

    private IEnumerator ConditionalDelayDestroy()
    {
        yield return new WaitForSeconds(1f);
        while (currentTarget)
        {
            yield return null;
        }

        Destroy();
    }

    private void Update()
    {
        if (m_target)
        {
            if (m_objectFollow.OnTarget)
            {
                m_anim.SetBool("Death", true);
            }
        }
    }
}
