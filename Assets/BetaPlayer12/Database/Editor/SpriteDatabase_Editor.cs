using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpriteDatabase))]
public class SpriteDatabase_Editor : IDatabase_Editor {

    private SerializedProperty m_overrideSpriteProp = null;

    protected override void DisplayAdditionalFoldoutDetails(SerializedProperty entryProp)
    {
        var entryItemProp = entryProp.FindPropertyRelative("m_sprite");

        DisplayTexture("Sprite: ", entryItemProp, true);
    }

    protected override void DisplayEditableFoldout()
    {
        EditorGUILayout.PropertyField(m_overrideSpriteProp);
    }

    protected override void OnLoadModule()
    {
        m_overrideSpriteProp = m_serializedObject.FindProperty("m_overrideSprite");
    }

    protected override void OnUnloadModule()
    {
        
    }

    protected override void OverrideEditableValues(SerializedProperty entryProp)
    {
        var entryItemProp = entryProp.FindPropertyRelative("m_sprite");

        m_overrideSpriteProp.objectReferenceValue = entryItemProp.objectReferenceValue;
    }

    protected override void SaveAdditionalEntryInfo(SerializedProperty editedEntryProp)
    {
        var entryItemProp = editedEntryProp.FindPropertyRelative("m_sprite");

        entryItemProp.objectReferenceValue = m_overrideSpriteProp.objectReferenceValue;
    }
}
