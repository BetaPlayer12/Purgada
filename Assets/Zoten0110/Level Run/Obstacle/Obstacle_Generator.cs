using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Generator : MonoBehaviour
{

    public Collider2D[] m_spawnArea;
    public int m_number;
    public bool m_override;

    private int m_currentSpawnIndex;
    // Use this for initialization
    void Start()
    {
        var levelConstructor = LevelConstructor.Instance;
        m_currentSpawnIndex = 0;
        var bounds = m_spawnArea[m_currentSpawnIndex].bounds.extents.x;

        if (m_override == false)
        {
            m_number = levelConstructor.GetObstacleSpawnCount();
        }

        if (GlobalGameSettings.Instance.enableObstacleGenerator)
        {

            for (int i = 0; i < m_number; i++)
            {
                var instance = Instantiate(levelConstructor.GetObstacle()) as GameObject;
                instance.transform.parent = m_spawnArea[m_currentSpawnIndex].transform;
                var collider = instance.GetComponentInChildren<Collider2D>();

                var position = m_spawnArea[m_currentSpawnIndex].transform.position.x + Random.Range(-bounds, bounds);
                instance.transform.position = new Vector3(position, 1.4f, 0f);
                instance.transform.localPosition = new Vector3(instance.transform.localPosition.x, 1.4f, 0f);
                instance.transform.rotation = m_spawnArea[m_currentSpawnIndex].transform.rotation;

                m_currentSpawnIndex++;
                m_currentSpawnIndex = m_currentSpawnIndex.RotateIndex(0, m_spawnArea.Length);
            }
        }
    }
}
