using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class IDatabase_Editor : Editor {

    protected enum ViewingType
    {
        All,
        Edit,
        Append,
        Specific,
        Searched,
        _Count,
    }

    protected IDatabase m_systemScript;
    protected SerializedObject m_serializedObject;


    protected string m_searchName = "";
    protected ViewingType m_viewingType = ViewingType.All;
    protected int m_editIndex;

    protected SerializedProperty m_entriesProp = null;
    private SerializedProperty m_overrideIDProp = null;
    private SerializedProperty m_overrideNameProp = null;

    protected abstract int autoAssignedID { get; set; }

    //Foldouts
    protected List<bool> m_entryFoldouts = null;

    void OnEnable()
    {
        m_systemScript = target as IDatabase;
        m_serializedObject = serializedObject;
        m_viewingType = ViewingType.All;

        m_overrideIDProp = m_serializedObject.FindProperty("m_overrideID");
        m_overrideNameProp = m_serializedObject.FindProperty("m_overrideName");

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
                m_systemScript.ResetOverride(autoAssignedID);
                m_viewingType = ViewingType.Append;
                return;
            }
            if (GUILayout.Button("Clear"))
            {
                autoAssignedID = 0;
                m_entryFoldouts = null;
                m_systemScript.Clear();
                m_viewingType = ViewingType.All;
                return;
            }
            EditorGUILayout.EndHorizontal();
        }

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
            autoAssignedID++;

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
                if (index == entry.iD)
                {
                    Debug.Log(index + " == " + entry.iD);
                    DisplayFoldout(i);
                }
            }
            else if (m_systemScript.isInDatabase(m_searchName, i))
            {
                DisplayFoldout(i);
            }
        }
    }

    protected void DisplayBaseEditableFoldout() {
        EditorGUILayout.LabelField("ID: ", m_overrideIDProp.intValue.ToString());
        m_overrideNameProp.stringValue = EditorGUILayout.TextField("Name: ", m_overrideNameProp.stringValue);
    }

    protected void DisplayFoldout(int index)
    {
        var entryProp = m_entriesProp.GetArrayElementAtIndex(index);

        var entryIDProp = entryProp.FindPropertyRelative("m_iD");
        var entryNameProp = entryProp.FindPropertyRelative("m_name");

        //Foldout
        {
            EditorGUILayout.BeginHorizontal();
            //Need to create a marker to know if it has pic or no
            FoldoutNameDisplay(index, entryProp,entryNameProp, entryIDProp);


            if (GUILayout.Button("Edit", GUILayout.MaxWidth(50)))
            {
                m_overrideIDProp.intValue = entryIDProp.intValue;
                m_overrideNameProp.stringValue = entryNameProp.stringValue == null ? "" : entryNameProp.stringValue;
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
        var entryIDProp = entryPro.FindPropertyRelative("m_iD");
        var entryNameProp = entryPro.FindPropertyRelative("m_name");
        entryIDProp.intValue = m_overrideIDProp.intValue;
        entryNameProp.stringValue = m_overrideNameProp.stringValue;
        SaveAdditionalEntryInfo(entryPro);
    }

    protected bool isIDtaken(int id)
    {

        for (int i = 0; i < m_systemScript.Count; i++)
        {
            if (m_systemScript[i].iD == id)
                return true;
        }
        return false;
    }

//<<<<<<< HEAD
        //return false;
//=======
    protected virtual void FoldoutNameDisplay(int index, SerializedProperty entryProp,SerializedProperty entryNameProp, SerializedProperty entryIDProp)
    {
        m_entryFoldouts[index] = EditorGUILayout.Foldout(m_entryFoldouts[index], entryNameProp.stringValue + " (ID: " + entryIDProp.intValue + " )");
//>>>>>>> d9691833ef733b3c125237353d1eb7d172db847d
    }

    protected void DisplayTexture(string label, SerializedProperty entryProp, bool usePrefabImage = false)
    {
        if (entryProp.objectReferenceValue)
        {
            Texture2D texturePreview = usePrefabImage?AssetPreview.GetAssetPreview(entryProp.objectReferenceValue) :((Sprite)entryProp.objectReferenceValue).texture;
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label+": ");
            GUILayout.Label(new GUIContent(texturePreview), GUILayout.Width(100), GUILayout.Height(100));
            EditorGUILayout.EndHorizontal();
        }
        else
        {
            EditorGUILayout.LabelField(label+": ", "[No Image]");

        }
    }




    protected abstract void OnLoadModule();

    protected abstract void SaveAdditionalEntryInfo(SerializedProperty editedEntryProp);

    protected abstract void DisplayEditableFoldout();

    protected abstract void OverrideEditableValues(SerializedProperty entryProp);

    protected abstract void DisplayAdditionalFoldoutDetails(SerializedProperty entryProp);


}
