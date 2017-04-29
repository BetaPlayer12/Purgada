using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemDatabase))]
public class ItemDatabase_Editor : IDatabase_Editor
{
    protected int m_assigendID;

    private SerializedProperty m_overrideItemProp = null;

    protected override int autoAssignedID
    {
        get
        {
            return m_assigendID;
        }

        set
        {
            m_assigendID = value;
        }
    }

    protected override void DisplayAdditionalFoldoutDetails(SerializedProperty entryProp)
    {
        var entryItemProp = entryProp.FindPropertyRelative("m_item");
        DisplayTexture("Item", entryItemProp, true);
    }

    protected override void DisplayEditableFoldout()
    {
        EditorGUILayout.PropertyField(m_overrideItemProp);
    }

    protected override void OnLoadModule()
    {
        Debug.Log(m_serializedObject.FindProperty("m_itemDatabase"));
        m_entriesProp = m_serializedObject.FindProperty("m_itemDatabase");
        m_overrideItemProp = m_serializedObject.FindProperty("m_overrideItem");
    }

    protected override void OverrideEditableValues(SerializedProperty entryProp)
    {
        var entryItemProp = entryProp.FindPropertyRelative("m_item");

        m_overrideItemProp.objectReferenceValue = entryItemProp.objectReferenceValue;
    }

    protected override void SaveAdditionalEntryInfo(SerializedProperty editedEntryProp)
    {
        var entryItemProp = editedEntryProp.FindPropertyRelative("m_item");

        entryItemProp.objectReferenceValue = m_overrideItemProp.objectReferenceValue;
    }
}
