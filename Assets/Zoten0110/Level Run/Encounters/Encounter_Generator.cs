using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter_Generator : MonoBehaviour
{

    public Collider2D m_spawnArea;

    // Use this for initialization
    void Start()
    {
        var encounter = LevelConstructor.Instance.GetEncounter();

        if (encounter)
        {
            var bounds = m_spawnArea.bounds.extents.x;

            var instance = Instantiate(encounter) as GameObject;
            instance.transform.parent = transform;
            var collider = instance.GetComponentInChildren<Collider2D>();

            var position = m_spawnArea.transform.position.x + Random.Range(-bounds, bounds);
            instance.transform.position = new Vector3(position, 1.4f, 0f);
            instance.transform.localPosition = new Vector3(instance.transform.localPosition.x, 1.4f, 0f);

            var movementSensors  = instance.GetComponentsInChildren<EncounterMovementSensor>();
            for (int i = 0; i < movementSensors.Length; i++)
            {
                movementSensors[i].m_floor = m_spawnArea.gameObject;
            }
        }
    }

}
