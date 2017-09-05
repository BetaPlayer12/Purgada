using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash_Generator : MonoBehaviour
{
    public Collider2D[] m_spawnAreas;
    public int m_number;

    public bool m_override;

    private int m_currentSpawnIndex;
    private List<Collider2D> m_colliders;

    private const int m_maxTrys =10; //number of tries b4 they choosing a new spawn point

   

    private bool isTouching(Collider2D collider)
    {
        var suggestedExtent = collider.bounds.extents.x;
        var colliderXPosition = collider.transform.position.x;
        var suggestedPositiveExtent = colliderXPosition + suggestedExtent;
        var suggestedNegativeExtent = colliderXPosition - suggestedExtent;

        var targetExtent = 0f;
        var targetColliderXPosition = 0f;
        var targetPositiveExtent = 0f;
        var targetNegativeExtent = 0f;
        for (int i = 0; i < m_colliders.Count; i++)
        {
            var targetCollider = m_colliders[i];
            targetExtent = targetCollider.bounds.extents.x;
            targetColliderXPosition = targetCollider.transform.position.x;
            targetPositiveExtent = targetColliderXPosition + targetExtent;
            targetNegativeExtent = targetColliderXPosition - targetExtent;

            //Checks if extents are over lapping
            if ((suggestedPositiveExtent > targetNegativeExtent && suggestedPositiveExtent < targetPositiveExtent) ||
                (suggestedNegativeExtent > targetNegativeExtent && suggestedNegativeExtent < targetPositiveExtent) ||
                (targetPositiveExtent > suggestedNegativeExtent && targetPositiveExtent < suggestedNegativeExtent) ||
                (targetNegativeExtent > suggestedNegativeExtent && targetNegativeExtent < suggestedNegativeExtent))
            {
                Debug.LogWarning("Waring");
                return true;
            }
        }

        return false;
    }

    // Use this for initialization
    void Start()
    {
        m_colliders = new List<Collider2D>();

        var levelConstructor = LevelConstructor.Instance;

        m_currentSpawnIndex = 0;


        if (m_override == false)
        {
            m_number = levelConstructor.GetTrashSpawnCount();
        }

        if (GlobalGameSettings.Instance.enableTrashGenerator)
        {
            for (int i = 0; i < m_number; i++)
            {
                var instance = Instantiate(levelConstructor.GetTrash()) as GameObject;
             
                var collider = instance.GetComponentInChildren<Collider2D>();

                var bounds = m_spawnAreas[m_currentSpawnIndex].bounds.extents.x;

                int tries = 0;
                int spawnCountTried = 0;

                //Tries to spawn trash away from other trash
                do
                {
                    var position = m_spawnAreas[m_currentSpawnIndex].transform.position.x + Random.Range(-bounds, bounds);
                    instance.transform.parent = m_spawnAreas[m_currentSpawnIndex].transform;
                    instance.transform.position = new Vector3(position, 1.4f, 0f);
                    instance.transform.localPosition = new Vector3(instance.transform.localPosition.x, 1.4f, 0f);
                    instance.transform.rotation = Quaternion.identity;
                    tries++;

                    //If all tries is exhausted
                    if (tries == 10)
                    {
                        //If all spawns Exhausted
                        if (spawnCountTried == m_spawnAreas.Length)
                        {
                            //Quit
                            return;
                        }
                        //Looks for available Spaawn Areas
                        else
                        {
                            
                            tries = 0;
                            spawnCountTried++;
                            m_currentSpawnIndex++;
                            m_currentSpawnIndex = m_currentSpawnIndex.RotateIndex(0, m_spawnAreas.Length);
                        }
                    }

                } while (isTouching(collider));


                m_colliders.Add(collider);
                m_currentSpawnIndex++;
                m_currentSpawnIndex = m_currentSpawnIndex.RotateIndex(0, m_spawnAreas.Length);
            }
        }
    }
}
