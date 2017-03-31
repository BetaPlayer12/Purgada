using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectAccessExtention
{
    #region Non_Recursive
    /// <summary>
    /// Returns the Parent Object of the gameobject, if there is none it returns null
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="name">Name of the Parent</param>
    /// <returns></returns>
    public static GameObject FindParent(this GameObject gameObject, string name) =>
        gameObject.transform.FindParent(name).gameObject;


    /// <summary>
    /// Returns the children of the gameobject that starts with the Name indicated, if there is none it returns null
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="startingName">Starting Name of the Children</param>
    /// <returns></returns>
    public static GameObject[] FindChildrenThatStartsWith(this GameObject gameObject, string startingName)
    {
        List<GameObject> m_listTransform = new List<GameObject>();
        var transform = gameObject.transform;
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if (child.name.StartsWith(startingName))
            {
                m_listTransform.Add(child.gameObject);
            }
        }

        return m_listTransform.ToArray();
    }

    /// <summary>
    /// Returns the children of the gameobject that starts with the Name indicated, if there is none it returns null
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="endingName">Starting Name of the Children</param>
    /// <returns></returns>
    public static GameObject[] FindChildrenThatEndsWith(this GameObject gameObject, string endingName)
    {
        List<GameObject> m_listTransform = new List<GameObject>();
        var transform = gameObject.transform;
        for (int i = 0; i < transform.childCount; i++)
        {

            var child = transform.GetChild(i);
            if (child.name.EndsWith(endingName))
            {

                m_listTransform.Add(child.gameObject);
            }
        }
        return m_listTransform.ToArray();
    }

    /// <summary>
    /// Returns Children that contains the specified name, if there is none it returns null
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="name">Specified name that the child will contain</param>
    /// <returns></returns>
    public static GameObject[] FindChildrenThatContains(this GameObject gameObject, string name)
    {
        List<GameObject> m_listTransform = new List<GameObject>();
        var transform = gameObject.transform;
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);

            if (child.name.Contains(name))
            {
                m_listTransform.Add(child.gameObject);
            }
        }
        return m_listTransform.ToArray();
    }
    #endregion
}
