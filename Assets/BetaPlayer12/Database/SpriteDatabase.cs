using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDatabase : IDatabase<SpriteDatabase.SpriteEntry> {

    [System.Serializable]
    public class SpriteEntry : IDatabaseEntry
    {
        [SerializeField]
        private Sprite m_sprite;

        public Sprite sprite { get { return m_sprite; } }
    }

    [SerializeField]
    private List<SpriteEntry> m_entries;

    public override List<SpriteEntry> entries
    {
        get
        {
            return m_entries;
        }
    }

#if UNITY_EDITOR 
    [SerializeField]
    private Sprite m_overrideSprite;

    public override void ResetOverrides()
    {
        m_overrideSprite = null;
    }

    public override void Clear()
    {
        m_entries = new List<SpriteEntry>();
    }
#endif
}
