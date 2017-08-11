using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter_Generator : MonoBehaviour {

    [System.Serializable]
    public class EncounterInfo
    {
        [SerializeField][Range(0, 100)]
        private int m_instanceRate;
        [SerializeField]
        private GameObject m_encounter;

        public int instanceRate { get { return m_instanceRate; } }
        public GameObject encounter { get { return m_encounter; } }
    }

    public Collider2D m_spawnArea;

    public EncounterInfo[] m_encounterList;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < m_encounterList.Length; i++)
        {
            var willInstansiate = m_encounterList[i].instanceRate >= Random.Range(0, 100);

            if (willInstansiate)
            {
                var encounter = m_encounterList[i].encounter;
                var bounds = m_spawnArea.bounds.extents.x;

                var instance = Instantiate(encounter) as GameObject;
                instance.transform.parent = transform;
                var collider = instance.GetComponentInChildren<Collider2D>();

                var position = m_spawnArea.transform.position.x + Random.Range(-bounds, bounds);
                instance.transform.position = new Vector3(position, 1.4f, 0f);
                instance.transform.localPosition = new Vector3(instance.transform.localPosition.x, 1.4f, 0f);
            }
        }
    }
}
