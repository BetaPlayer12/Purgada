using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Generator : MonoBehaviour
{

    public Collider2D m_spawnArea;
    public int m_number;

    // Use this for initialization
    void Start()
    {
        var levelConstructor = LevelConstructor.Instance;
        var bounds = m_spawnArea.bounds.extents.x;
        if (GlobalGameSettings.enableObstacleGenerator)
        {

            for (int i = 0; i < m_number; i++)
            {
                var instance = Instantiate(levelConstructor.GetObstacle()) as GameObject;
                instance.transform.parent = transform;
                var collider = instance.GetComponentInChildren<Collider2D>();

                var position = m_spawnArea.transform.position.x + Random.Range(-bounds, bounds);
                instance.transform.position = new Vector3(position, 1.4f, 0f);
                instance.transform.localPosition = new Vector3(instance.transform.localPosition.x, 1.4f, 0f);
            }
        }
    }
}
