using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash_Generator : MonoBehaviour
{
    public Collider2D m_spawnArea;
    public int m_number;

    private List<Collider2D> m_colliders;

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

        var bounds = m_spawnArea.bounds.extents.x;


        if (GlobalGameSettings.Instance.enableTrashGenerator)
        {
            for (int i = 0; i < m_number; i++)
            {
                var instance = Instantiate(levelConstructor.GetTrash()) as GameObject;
                instance.transform.parent = transform;
                var collider = instance.GetComponentInChildren<Collider2D>();
                do
                {
                    var position = m_spawnArea.transform.position.x + Random.Range(-bounds, bounds);
                    instance.transform.position = new Vector3(position, 1.4f, 0f);
                    instance.transform.localPosition = new Vector3(instance.transform.localPosition.x, 1.4f, 0f);
                } while (isTouching(collider));

                m_colliders.Add(collider);
            }
        }
    }
}
