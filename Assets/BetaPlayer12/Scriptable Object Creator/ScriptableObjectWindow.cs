using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Linq;

public class ScriptableObjectWindow : EditorWindow
{

    private string[] m_scriptableObjects;

    private int m_index = 0;
    private int m_targetIndex = 0;
    private string m_assetName = "";
    private string m_pathFile = "";


    [MenuItem("Assets/Create/Scriptable Object")]
    private static void OpenWindow()
    {
        ScriptableObjectWindow window = EditorWindow.GetWindow<ScriptableObjectWindow>();
        window.Show();
    }


    //Window Specific Scripts

    void Awake()
    {
        titleContent = new GUIContent("Scriptable Object Creator");

        var allClass = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();
        var scriptable = (from System.Type type in allClass where type.IsSubclassOf(typeof(ScriptableObject)) select type).ToArray();
        m_scriptableObjects = new string[scriptable.Length];

        for (int i = 0; i < scriptable.Length; i++)
            m_scriptableObjects[i] = scriptable[i].ToString();
    }

    private void OnSelectionChange()
    {

    }

    private void OnGUI()
    {
        GUILayout.Label("Create Scriptable Object", EditorStyles.boldLabel);
        m_index = EditorGUILayout.Popup("Scriptable Object", m_targetIndex, m_scriptableObjects);
        m_assetName = EditorGUILayout.TextField("Asset Name", m_assetName == "" ? m_scriptableObjects[m_targetIndex] : m_assetName);
        m_pathFile = EditorGUILayout.TextField(new GUIContent("File Path", "Path where the Object is created inside the Assets Folder, Non-existent Folders will be created \n Sperate folder names with '/' "), m_pathFile);
        var filePath = m_pathFile == "" ? "Assets" : "Assets/" + m_pathFile;


        if (!AssetDatabase.IsValidFolder(filePath))
        {
            EditorGUILayout.LabelField("Path: " + filePath + " does not exist",EditorStyles.helpBox);
        }

        if (m_index != m_targetIndex)
        {
            m_targetIndex = m_index;
            m_assetName = m_scriptableObjects[m_targetIndex];
        }

        if (GUILayout.Button("Create"))
        {
            CreateAsset();
        };

    }

    private void CreateAsset()
    {
        var asset = ScriptableObject.CreateInstance(m_scriptableObjects[m_index]);

        var filePath = m_pathFile == ""?"Assets":"Assets/" + m_pathFile;

        if (!AssetDatabase.IsValidFolder(filePath))
        {
            CreateValidFolder(GetParentPath(filePath),m_pathFile);
        }

        filePath += "/";
        AssetDatabase.CreateAsset(asset, filePath + m_assetName + ".asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }

    private void CreateValidFolder(string parent, string path)
    {
        if (!AssetDatabase.IsValidFolder(parent))
        {
            CreateValidFolder(GetParentPath(parent), GetParentPath(path));
        }

        Debug.Log("Creating "+ path + " Folder at path \"" + parent+"\"");

        var splitPath = path.Split('/');
        var folderName = splitPath[splitPath.Length - 1];

        AssetDatabase.CreateFolder(parent, folderName);
    }

    private string GetParentPath(string path)
    {
        if (path.Contains('/'))
        {
            var pathSplit = path.Split('/');

            string shortenedPath = "";
            for (int i = 0; i < pathSplit.Length - 1; i++)
            {
                if (i > 0)
                    shortenedPath += "/";

                shortenedPath += pathSplit[i];
            }
            return shortenedPath;
        }
        return path; ;
    }
}
