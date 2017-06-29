//Requires C# 6

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ComponentAccessExtention {

    /// <summary>
    /// Checks if MonoBehaviour has the component
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_mono"></param>
    /// <returns></returns>
    public static bool HasComponent<T>(this Component _mono) where T : Component =>
        _mono.GetComponent<T>() != null;


    /// <summary>
    /// Gathers the components indicated from the list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_mono"></param>
    /// <param name="list"></param>
    /// <returns></returns>
    public static T[] GetComponentOfList<T>(this Component _mono, List<Component> list) where T : Component
    {
        List<T> newlist = new List<T>();
        for (int i = 0; i < list.Count; i++)
        {
            var element = list[i];
            if (element.HasComponent<T>())
            {
                newlist.Add(element.GetComponent<T>());
            }
        }
        return newlist.ToArray();
    }

    /// <summary>
    /// Gathers the components indicated from the array
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_mono"></param>
    /// <param name="array"></param>
    /// <returns></returns>
    public static T[] GetComponentOfArray<T>(this Component _mono, Component[] array) where T : Component
    {
        List<T> newlist = new List<T>();
        for (int i = 0; i < array.Length; i++)
        {
            var element = array[i];
            if (element.HasComponent<T>())
            {
                newlist.Add(element.GetComponent<T>());
            }
        }
        return newlist.ToArray();
    }

    #region Parent_Children
    /// <summary>
    /// Gets component of the first child that has the name indicated
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_component"></param>
    /// <param name="name">name of the child</param>
    /// <returns></returns>
    public static T GetComponentOfParent<T>(this Component _component, string name) where T : Component =>
        _component.transform.FindParent(name).GetComponent<T>();

    /// <summary>
    /// Gets component of the first child that has the name indicated
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_component"></param>
    /// <param name="name">name of the child</param>
    /// <returns></returns>
	public static T GetComponentOfChild<T>(this Component _component, string name) where T: Component =>
        _component.transform.Find(name).GetComponent<T>();

    /// <summary>
    /// Gets component of the children that starts with the name indicated
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_component"></param>
    /// <param name="name">name of the child</param>
    /// <returns></returns>
    public static T[] GetComponentOfChildrenThatStartWith<T>(this Component _component, string name) where T : Component =>
       _component.GetComponentOfArray<T>(_component.transform.FindChildrenThatStartsWith(name));


    /// <summary>
    /// Gets component of the children that ends with the name indicated
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_component"></param>
    /// <param name="name">name of the child</param>
    /// <returns></returns>
    public static T[] GetComponentOfChildrenThatEndsWith<T>(this Component _component, string name) where T : Component =>
       _component.GetComponentOfArray<T>(_component.transform.FindChildrenThatEndsWith(name));


    /// <summary>
    /// Gets component of the children that contains the name indicated
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_component"></param>
    /// <param name="name">name of the child</param>
    /// <returns></returns>
    public static T[] GetComponentOfChildrenThatContains<T>(this Component _component, string name) where T : Component =>
       _component.GetComponentOfArray<T>(_component.transform.FindChildrenThatContains(name));
    #endregion
}
