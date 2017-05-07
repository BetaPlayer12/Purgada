using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generates and Stores information that will be instantiated next
/// </summary>
public class LevelConstructor : Singleton<LevelConstructor>
{

    private DatabaseSystem m_databaseSystem;

#if false //Use this if Queue<T> wont work well
    private class ItemInfo
    {
        private GameObject[] m_itemList; //Contains Items that will be instantiated
        private int m_currentIndex = 0; // Current Index of the item to be instantiated
        private static int m_itemListSize = 5; //Size of Item List
        private bool m_isFull; //Is true when there is no Null value in the list

        public ItemInfo(GameObject[] newList)
        {
            m_itemList = new GameObject[m_itemListSize];

            for (int i = 0; i < m_itemList.Length; i++)
            {
                if (i < newList.Length)
                {
                    m_itemList[i] = newList[i];
                }
                else
                {
                    m_itemList[i] = null;
                }
            }

            m_isFull = m_itemList[m_itemListSize - 1] != null;
        }

        public GameObject GetItem()
        {
            var chosenItem = m_itemList[m_currentIndex];
            m_currentIndex++;
            Wrap_Integer.RotateIndex(ref m_currentIndex, 0, m_itemListSize);
            return chosenItem;
        }

        public void AddItem(GameObject item)
        {
            if (m_isFull)
            {
                var prevIndex = m_currentIndex - 1;
                Wrap_Integer.RotateIndex(ref prevIndex, 0, m_itemListSize);
                m_itemList[m_currentIndex] = item;
            }
            else
            {
                var nullIndex = 0;
                while(m_itemList[nullIndex] != null)
                {
                    nullIndex++;
                }

                m_itemList[nullIndex] = item;

                if(nullIndex == m_itemListSize - 1)
                {
                    m_isFull = true;
                }
            }
        }
    }
#endif
    private Queue<GameObject> m_platformQueue;
    private Queue<GameObject> m_trashQueue;
    private Queue<GameObject> m_obstacleQueue;


    public GameObject GetPlatform()
    {
        var index = 0;
        m_platformQueue.Enqueue(m_databaseSystem.GetEntryOf<ItemDatabase.ItemEntry>(index).item);
        return m_platformQueue.Dequeue();
    }

    public GameObject GetTrash()
    {
        EnqueueTrash();
        return m_trashQueue.Dequeue();
    }

    private void EnqueueTrash()
    {
        var index = Random.Range(0, m_databaseSystem.GetSize<TrashDatabase>());
        m_trashQueue.Enqueue(m_databaseSystem.GetEntryOf<TrashDatabase.TrashEntry>(index).trash);
    }

    public GameObject GetObstacle()
    {
        EnqueueObstsacle();
        return m_obstacleQueue.Dequeue();
    }

    private void EnqueueObstsacle()
    {
        var obstacleDatabase = m_databaseSystem.GetDatabase<ObstacleDatabase>();
        m_obstacleQueue.Enqueue(obstacleDatabase.GetRandomObstacle());
    }

    private void InitializeQueues()
    {
        int queueSize = 5;
        for (int i = 0; i < queueSize; i++)
        {
            EnqueueObstsacle();
            EnqueueTrash();
        }
    }

    void Start()
    {
        m_platformQueue = new Queue<GameObject>();
        m_trashQueue = new Queue<GameObject>();
        m_obstacleQueue = new Queue<GameObject>();

        m_databaseSystem = GameManager.Instance.GetSystem<DatabaseSystem>();
        InitializeQueues();
    }
}
