using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using System;
public abstract class IDatabase_Editor : Editor
{

    protected enum ViewingType
    {
        All,
        Edit,
        Append,
        Specific,
        Searched,
        DatabaseNameEdit,
        _Count,
    }

    protected IBaseDatabase m_systemScript;
    protected SerializedObject m_serializedObject;

    private int m_autoAssignedID = 0;
    protected string m_searchName = "";
    protected ViewingType m_viewingType = ViewingType.All;
    protected int m_editIndex;

    private SerializedProperty m_databaseName = null;

    protected SerializedProperty m_entriesProp = null;
    private string m_overrideDatabaseName ="";
    private int m_overrideID;
    private string m_overrideName;

    //Foldouts
    protected List<bool> m_entryFoldouts = null;

    void OnEnable()
    {
        m_systemScript = target as IBaseDatabase;
        m_serializedObject = serializedObject;
        m_viewingType = ViewingType.All;

        m_databaseName = m_serializedObject.FindProperty("m_databaseName");
        m_entriesProp = m_serializedObject.FindProperty("m_entries");
        m_overrideID = 0;
        m_overrideName = "";



        OnLoadModule();

        if(m_entriesProp.arraySize != 0)
        m_systemScript.SortID();
    }

    void OnDisable()
    {
        m_systemScript = null;
    }

    public override void OnInspectorGUI()
    {
        //Updating Original Script
        {
            m_serializedObject.Update();
            EditorGUIExt.ScriptPropertyField(serializedObject);
            m_serializedObject.ApplyModifiedProperties();
        }

        serializedObject.Update();
        EditorGUIExt.ScriptAndAssetPropertyFields(serializedObject);

        EditorGUILayout.Space();

        if (m_viewingType == ViewingType.DatabaseNameEdit)
        {

            m_overrideDatabaseName = EditorGUILayout.TextField("Database Name: ", m_overrideDatabaseName);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Save"))
            {
                m_databaseName.stringValue = m_overrideDatabaseName;
                m_viewingType = ViewingType.All;
            }

            if (GUILayout.Button("Cancel"))
            {
                m_overrideDatabaseName = m_databaseName.stringValue;
                m_viewingType = ViewingType.All;
            }
                EditorGUILayout.EndHorizontal();
        }
        else {

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Database Name: " + m_databaseName.stringValue);
            if (GUILayout.Button("Edit"))
            {
                m_viewingType = ViewingType.DatabaseNameEdit;
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("All"))
                {
                    m_viewingType = ViewingType.All;
                }
                if (GUILayout.Button("Search"))
                {
                    m_searchName = "";
                    m_viewingType = ViewingType.Searched;
                }
                EditorGUILayout.EndHorizontal();
            }

            if (m_viewingType == ViewingType.Searched)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Search: ");
                m_searchName = GUILayout.TextField(m_searchName, GUILayout.MaxWidth(500));
                EditorGUILayout.EndHorizontal();
            }

            if (m_viewingType != ViewingType.Append)

            //Database Modifier
            {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Add"))
                {
                    m_overrideID = m_autoAssignedID;
                    m_overrideName = "";
                    m_systemScript.ResetOverrides();
                    m_viewingType = ViewingType.Append;
                    return;
                }
                if (GUILayout.Button("Clear"))
                {
                    m_autoAssignedID = 0;
                    m_entryFoldouts = null;
                    m_systemScript.Clear();
                    m_systemScript.ResetOverrides();
                    m_viewingType = ViewingType.All;
                    return;
                }
                if (GUILayout.Button("Fix IDs"))
                {
                    for (int i = 0; i < m_entriesProp.arraySize; ++i)
                    {
                        var entryProp = m_entriesProp.GetArrayElementAtIndex(i);

                        entryProp.FindPropertyRelative("m_ID").intValue = i;
                    }
                }
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.Space();

            if (m_viewingType != ViewingType.Append && m_viewingType != ViewingType.Edit && m_entriesProp.arraySize == 0)
            {
                EditorGUILayout.LabelField("Database is Empty");
                return;
            }
            switch (m_viewingType)
            {
                case ViewingType.All:
                    DisplayAll();
                    break;
                case ViewingType.Append:
                    DisplayAppend();
                    break;
                case ViewingType.Searched:
                    DisplaySearchName();
                    break;
                case ViewingType.Edit:
                    DisplayEdit();
                    break;
            }

        }

        serializedObject.ApplyModifiedProperties();
    }

    protected void DisplayAll()
    {
        CheckFoldouts();
        for (int i = 0; i < m_entriesProp.arraySize; i++)
        {
            DisplayFoldout(i);
        }
    }

    protected void DisplayAppend()
    {
        DisplayBaseEditableFoldout();
        DisplayEditableFoldout();

        //Save and Cancel Buttons
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Append"))
        {
            m_entriesProp.arraySize++;
            m_autoAssignedID++;

            var latestEntryProp = m_entriesProp.GetArrayElementAtIndex(m_entriesProp.arraySize - 1);
            SaveEntryProp(latestEntryProp);

            CheckFoldouts();
            m_viewingType = ViewingType.All;
        }

        if (GUILayout.Button("Cancel"))
        {
            m_viewingType = ViewingType.All;
        }

        EditorGUILayout.EndHorizontal();
    }

    protected void DisplayEdit()
    {
        DisplayBaseEditableFoldout();
        DisplayEditableFoldout();

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Save", GUILayout.MaxWidth(50)))
        {
            var editedEntryProp = m_entriesProp.GetArrayElementAtIndex(m_editIndex);
            SaveEntryProp(editedEntryProp);
            m_viewingType = ViewingType.All;
            return;
        }

        if (GUILayout.Button("Cancel", GUILayout.MaxWidth(50)))
        {
            m_viewingType = ViewingType.All;
            return;
        }

        EditorGUILayout.EndHorizontal();
    }


    protected void DisplaySearchName()
    {

        CheckFoldouts();
        var index = 0;
        for (int i = 0; i < m_systemScript.Count; i++)
        {
            var entry = m_systemScript[i];
            if (int.TryParse(m_searchName, out index))
            {
                if (index == entry.ID)
                {
                    Debug.Log(index + " == " + entry.ID);
                    DisplayFoldout(i);
                }
            }
            else if (m_systemScript.IsInDatabase(m_searchName, i))
            {
                DisplayFoldout(i);
            }
        }
    }

    protected void DisplayBaseEditableFoldout() {
        EditorGUILayout.LabelField("ID: ", m_overrideID.ToString());
        m_overrideName = EditorGUILayout.TextField("Name: ", m_overrideName);
    }

    protected void DisplayFoldout(int index)
    {
        var entryProp = m_entriesProp.GetArrayElementAtIndex(index);

        var entryIDProp = entryProp.FindPropertyRelative("m_ID");
        var entryNameProp = entryProp.FindPropertyRelative("m_name");

        //Foldout
        {
            EditorGUILayout.BeginHorizontal();
            //Need to create a marker to know if it has pic or no
            FoldoutNameDisplay(index, entryProp,entryNameProp, entryIDProp);


            if (GUILayout.Button("Edit", GUILayout.MaxWidth(50)))
            {
                m_overrideID = entryIDProp.intValue;
                m_overrideName = entryNameProp.stringValue == null ? "" : entryNameProp.stringValue;
                OverrideEditableValues(entryProp);
                m_viewingType = ViewingType.Edit;
                m_editIndex = index;
                return;
            }

            if (GUILayout.Button("Delete", GUILayout.MaxWidth(50)))
            {
                m_entriesProp.DeleteArrayElementAtIndex(index);
                m_entryFoldouts.RemoveAt(index);
                return;
            }


            EditorGUILayout.EndHorizontal();
        }

        //Foldout Details
        if (m_entryFoldouts[index])
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.LabelField("ID: ", entryIDProp.intValue.ToString());
            EditorGUILayout.LabelField("Name: ", entryNameProp.stringValue);
            DisplayAdditionalFoldoutDetails(entryProp);
            EditorGUI.indentLevel--;
        }
    }

    protected void CheckFoldouts()
    {
        if (m_entryFoldouts == null || m_entryFoldouts.Count != m_entriesProp.arraySize)
        {
            var newList = new List<bool>(m_entriesProp.arraySize);
            m_entryFoldouts = m_entryFoldouts == null ? new List<bool>() : m_entryFoldouts;

            for (int i = 0; i < m_entriesProp.arraySize; i++)
            {
                newList.Add(true);
                if (i < m_entryFoldouts.Count)
                    newList[i] = m_entryFoldouts[i];
            }

            m_entryFoldouts = newList;
        }
    }

    protected void SaveEntryProp(SerializedProperty entryPro) {
        var entryIDProp = entryPro.FindPropertyRelative("m_ID");
        var entryNameProp = entryPro.FindPropertyRelative("m_name");
        entryIDProp.intValue = m_overrideID;
        entryNameProp.stringValue = m_overrideName;
        SaveAdditionalEntryInfo(entryPro);
    }

    protected bool isIDtaken(int id)
    {

        for (int i = 0; i < m_systemScript.Count; i++)
        {
            if (m_systemScript[i].ID == id)
                return true;
        }
        return false;
    }

    protected virtual void FoldoutNameDisplay(int index, SerializedProperty entryProp,SerializedProperty entryNameProp, SerializedProperty entryIDProp)
    {
        m_entryFoldouts[index] = EditorGUILayout.Foldout(m_entryFoldouts[index], entryNameProp.stringValue + " (ID: " + entryIDProp.intValue + " )");
    }

    protected abstract void OnLoadModule();

    protected abstract void SaveAdditionalEntryInfo(SerializedProperty editedEntryProp);

    protected abstract void DisplayEditableFoldout();

    protected abstract void OverrideEditableValues(SerializedProperty entryProp);

    protected abstract void DisplayAdditionalFoldoutDetails(SerializedProperty entryProp);

    #region Special Functions
    protected void DisplayTexture(string label, SerializedProperty entryProp, bool usePrefabImage = false)
    {
        if (entryProp.objectReferenceValue)
        {
            Texture2D texturePreview = usePrefabImage ? AssetPreview.GetAssetPreview(entryProp.objectReferenceValue) : ((Sprite)entryProp.objectReferenceValue).texture;
            if(texturePreview == null)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(label + ": " + entryProp.objectReferenceValue.name);
                EditorGUILayout.EndHorizontal();
                return;
            }
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label + ": ");
            GUILayout.Label(new GUIContent(texturePreview), GUILayout.Width(100), GUILayout.Height(100));
            EditorGUILayout.EndHorizontal();
        }
        else
        {
            EditorGUILayout.LabelField(label + ": ", "[No Image]");

        }
    }

    protected void PlayClip(AudioClip clip)
    {
        Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
        Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
        MethodInfo method = audioUtilClass.GetMethod(
            "PlayClip",
            BindingFlags.Static | BindingFlags.Public,
            null,
            new System.Type[] {
                typeof(AudioClip)
            },
            null
        );
        method.Invoke(
            null,
            new object[] {
                clip
            }
        );
    }

    protected void StopAllClips()
    {
        Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
        Type audioUtilClass =
              unityEditorAssembly.GetType("UnityEditor.AudioUtil");
        MethodInfo method = audioUtilClass.GetMethod(
            "StopAllClips",
            BindingFlags.Static | BindingFlags.Public,
            null,
            new System.Type[] { },
            null
        );
        method.Invoke(
            null,
            new object[] { }
        );
    }
    #endregion


}
