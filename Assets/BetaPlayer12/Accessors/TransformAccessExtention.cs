using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformAccessExtention
{

    #region Non_Recursive
    /// <summary>
    /// Returns the Parent of the gameobject, if there is none it returns null
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="name">Name of the Parent</param>
    /// <returns></returns>
    public static Transform FindParent(this Transform transform, string name)
    {
        Transform parentObject = transform.parent;
        do
        {
            parentObject = parentObject.parent;
            if (parentObject.name == name)
            {
                return parentObject;
            }

        } while (parentObject);

        return null;
    }

    /// <summary>
    /// Returns the children of the gameobject that starts with the Name indicated, if there is none it returns null
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="startingName">Starting Name of the Children</param>
    /// <returns></returns>
    public static Transform[] FindChildrenThatStartsWith(this Transform transform, string startingName)
    {
        List<Transform> m_listTransform = new List<Transform>();

        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if (child.name.StartsWith(startingName))
            {
                m_listTransform.Add(child);
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
    public static Transform[] FindChildrenThatEndsWith(this Transform transform, string endingName)
    {
        List<Transform> m_listTransform = new List<Transform>();

        for (int i = 0; i < transform.childCount; i++)
        {

            var child = transform.GetChild(i);
            if (child.name.EndsWith(endingName))
            {

                m_listTransform.Add(child);
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
    public static Transform[] FindChildrenThatContains(this Transform transform, string name)
    {
        List<Transform> m_listTransform = new List<Transform>();

        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);

            if (child.name.Contains(name))
            {
                m_listTransform.Add(child);
            }
        }
        return m_listTransform.ToArray();
    }
    #endregion
}
