using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_Generator : MonoBehaviour
{

    public Collider2D m_spawnArea;

    // Use this for initialization
    void Start()
    {
        var powerup = LevelConstructor.Instance.GetPowerup();

        if (powerup)
        {
            var bounds = m_spawnArea.bounds.extents.x;

            var instance = Instantiate(powerup) as GameObject;
            instance.transform.parent = transform;
            var collider = instance.GetComponentInChildren<Collider2D>();

            var position = m_spawnArea.transform.position.x + Random.Range(-bounds, bounds);
            instance.transform.position = new Vector3(position, 1.4f, 0f);
            instance.transform.localPosition = new Vector3(instance.transform.localPosition.x, 1.4f, 0f);
        }
    }

}
