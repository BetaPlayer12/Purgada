using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemDatabase))]
public class ItemDatabase_Editor : IDatabase_Editor
{
    private SerializedProperty m_overrideItemProp = null;

    protected override void DisplayAdditionalFoldoutDetails(SerializedProperty entryProp)
    {
        DisplayTexture("Item: ", m_overrideItemProp, true);
    }

    protected override void DisplayEditableFoldout()
    {
        EditorGUILayout.PropertyField(m_overrideItemProp);
    }

    protected override void OnLoadModule()
    {
        m_overrideItemProp = m_serializedObject.FindProperty("m_overrideItem");
    }

    protected override void OnUnloadModule()
    {
        throw new NotImplementedException();
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
