using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generates and Stores information that will be instantiated next
/// </summary>
public class LevelConstructor : Singleton<LevelConstructor>
{
    private DatabaseSystem m_databaseSystem;

    private DatabaseSystem databaseSystem
    {
        get
        {
            if (m_databaseSystem == null)
            {
                m_databaseSystem = GameManager.Instance.GetSystem<DatabaseSystem>();

            }
            return m_databaseSystem;
        }
    }

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

    [Header("Trash Settings")]
    [SerializeField][Range(0,100)]
    private int m_trashSpawnPercentage; //Percent of If trash will spawn
    [SerializeField]
    private int m_minTrashCount;
    [SerializeField]
    private int m_maxTrashCount; 

    [Space]
    [Header("Obstacle Settings")]
    [SerializeField][Range(0,100)]
    private int m_obstacleSpawnPercentage; //Percent of If obstacle will spawn
    [SerializeField]
    private int m_minObstacleCount; 
    [SerializeField]
    private int m_maxObstacleCount; 

    [Space]
    [Header("encounter Settings")]
    [SerializeField]
    [Range(0, 100)]
    private int m_encounterSpawnPercentage; //Percent of If an encounter will spawn
    [SerializeField]
    private ObjectDropRate m_encounterSpawnInfo;

    [Space]
    [Header("powerup Settings")]
    [SerializeField]
    [Range(0, 100)]
    private int m_powerupSpawnPercentage; //Percent of If powerups will spawn
    [SerializeField]
    private ObjectDropRate m_powerupDropInfo;


    [Space]
    [Header("pickup Settings")]
    [SerializeField]
    [Range(0, 100)]
    private int m_pickupSpawnPecentage; //Percent of If pickup will spawn
    [SerializeField]
    private ObjectDropRate m_pickupDropInfo;


    public int GetTrashSpawnCount()
    {
        if(m_trashSpawnPercentage > Random.Range(0, 100))
        {
            return Random.Range(m_minTrashCount, m_maxTrashCount);
        }

        return 0;
    }


    public int GetObstacleSpawnCount()
    {
        if (m_obstacleSpawnPercentage> Random.Range(0, 100))
        {
            return Random.Range(m_minObstacleCount, m_maxObstacleCount);
        }

        return 0;
    }


    public GameObject GetPlatform()
    {
        EnqueuePlatform();
        return m_platformQueue.Dequeue();
    }

    private void EnqueuePlatform()
    {
        var index = Random.Range(0, databaseSystem.GetSize<PlatformDatabase>());
        Debug.Log("Queued platform index +" + index.ToString());
        m_platformQueue.Enqueue(databaseSystem.GetEntryOf<PlatformDatabase.PlatformEntry>(index).platform);
    }

    public GameObject GetTrash()
    {
        EnqueueTrash();
        return m_trashQueue.Dequeue();
    }

    private void EnqueueTrash()
    {
        var index = Random.Range(0, databaseSystem.GetSize<TrashDatabase>());
        m_trashQueue.Enqueue(databaseSystem.GetEntryOf<TrashDatabase.TrashEntry>(index).trash);
    }

    public GameObject GetObstacle()
    {
        EnqueueObstsacle();
        return m_obstacleQueue.Dequeue();
    }

    public GameObject GetEncounter()
    {
        if (Random.Range(0, 100) <= m_encounterSpawnPercentage)
        {
            return m_encounterSpawnInfo.GetInstanceObject();
        }

        return null;
    }

    public GameObject GetPickup()
    {
        if(Random.Range(0,100) <= m_pickupSpawnPecentage)
        {
           return  m_pickupDropInfo.GetInstanceObject();
        }

        return null;
    }

    public GameObject GetPowerup()
    {
        if (Random.Range(0, 100) <= m_powerupSpawnPercentage)
        {
            return m_powerupDropInfo.GetInstanceObject();
        }

        return null;
    }

    private void EnqueueObstsacle()
    {
        var obstacleDatabase = databaseSystem.GetDatabase<ObstacleDatabase>();
        m_obstacleQueue.Enqueue(obstacleDatabase.GetRandomObstacle());
    }

    private void InitializeQueues()
    {
        int queueSize = 5;
        for (int i = 0; i < queueSize; i++)
        {
            EnqueuePlatform();
            EnqueueObstsacle();
            EnqueueTrash();
        }
    }

    void Awake()
    {
        m_platformQueue = new Queue<GameObject>();
        m_trashQueue = new Queue<GameObject>();
        m_obstacleQueue = new Queue<GameObject>();
    }

    void Start()
    {
        InitializeQueues();
    }
}
