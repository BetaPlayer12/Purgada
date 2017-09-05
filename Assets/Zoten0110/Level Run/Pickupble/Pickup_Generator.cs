using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Generator : MonoBehaviour
{

    [System.Serializable]
    public class PickupInfo
    {
        [SerializeField]
        [Range(0, 100)]
        private int m_instanceRate;
        [SerializeField]
        private GameObject m_pickup;

        public int instanceRate { get { return m_instanceRate; } }
        public GameObject pickup { get { return m_pickup; } }
    }

    public Collider2D[] m_spawnArea;

    // Use this for initialization
    void Start()
    {
        var pickup = LevelConstructor.Instance.GetPickup();

        if (pickup)
        {
            for (int x = 0; x < m_spawnArea.Length; x++)
            {
                var willInstansiateHere = 100f >= Random.Range(0, 100);

                if (willInstansiateHere)
                {
                    var bounds = m_spawnArea[x].bounds.extents.x;

                    var instance = Instantiate(pickup) as GameObject;
                    instance.transform.parent = m_spawnArea[x].transform;
                    var collider = instance.GetComponentInChildren<Collider2D>();

                    var position = m_spawnArea[x].transform.position.x + Random.Range(-bounds, bounds);
                    instance.transform.position = new Vector3(position, 1.4f, 0f);
                    instance.transform.localPosition = new Vector3(instance.transform.localPosition.x, 1.4f, 0f);
                    break;
                }
            }

        }

    }
}
