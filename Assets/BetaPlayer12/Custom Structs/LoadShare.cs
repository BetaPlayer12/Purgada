using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Intended to be used with other Load objects
/// in a LoadShareManager
/// </summary>
/// Used inside the Editor script only
[System.Serializable]
public struct Load
{


    [SerializeField]
    private SerializedProperty m_value;         //Float from the main script
    [SerializeField]
    private bool m_isOpen;
    private float m_prevValue;
    private float m_maxValue;

    public Load(SerializedProperty prop)
    {
        m_value = prop;
        m_isOpen = true;
        m_prevValue = m_value.floatValue;
        m_maxValue = 100f;
    }

    public float load
    {
        get
        {
            return m_value.floatValue;
        }

        private set
        {
            m_value.floatValue = value;
        }
    }

    public bool canBeDeducted { get { return m_value.floatValue != 0f && m_isOpen; } }
    public bool canBeAdded { get { return m_value.floatValue != m_maxValue && m_isOpen; } }
    public bool isLocked { get { return !m_isOpen; } }

    public void SetMax(float max)
    {
        m_maxValue = max;
    }

    /// <summary>
    /// Makes sure that the value does not exceed max
    /// </summary>
    public void BindValue()
    {
        if (load > m_maxValue)
        {
            load = m_maxValue;
        }
    }

    /// <summary>
    /// Reverts current value to saved value
    /// </summary>
    public void Revert()
    {
        load = m_prevValue;
    }

    /// <summary>
    /// Force set the value
    /// </summary>
    /// <param name="value"></param>
    public void ForceValue(float value)
    {
        m_value.floatValue = value;
    }

    /// <summary>
    /// Deducts the value of load by amount
    /// </summary>
    /// <param name="amount"></param>
    /// <returns>Returns the amount that cannot be deduct since the value is now 0</returns>
    public float DeductLoad(float amount)
    {
        if (m_maxValue == 0f)
        {
            return 0;
        }

        amount = Mathf.Abs(amount);

        m_value.floatValue -= amount;
        Commit();

        if (m_value.floatValue < 0f)
        {
            float excess = Mathf.Abs(m_value.floatValue);
            m_value.floatValue = 0;
            return excess;
        }
        return 0;
    }

    /// <summary>
    /// Simulates what Deducting the load by amount would be like
    /// </summary>
    /// <param name="amount"></param>
    /// <returns>Returns the amount that cannot be deduct since the value is now 0 on simulation</returns>
    public float TryDeductLoad(float amount)
    {
        if (m_maxValue == 0f)
        {
            return 0;
        }

        float floatValue = m_value.floatValue;
        amount = Mathf.Abs(amount);

        floatValue -= amount;

        if (m_value.floatValue < 0f)
        {
            float excess = Mathf.Abs(floatValue);
            return excess;
        }

        return 0;
    }

    /// <summary>
    /// Adds the value of load by amount
    /// </summary>
    /// <param name="amount"></param>
    /// <returns>Returns the amount that cannot be add since the value is now MAXED</returns>
    public float AddLoad(float amount)
    {
        if (m_maxValue == 0f)
        {
            return 0;
        }

        amount = Mathf.Abs(amount);

        m_value.floatValue += amount;
        Commit();

        if (m_value.floatValue > m_maxValue)
        {
            float excess = m_value.floatValue - m_maxValue;
            m_value.floatValue = m_maxValue;
            return excess;
        }
        return 0;
    }

    /// <summary>
    /// Simulates what Adding the value of load by amount would be like
    /// </summary>
    /// <param name="amount"></param>
    /// <returns>Returns the amount that cannot be deduct since the value is now MAXED on simulation </returns>
    public float TryAddLoad(float amount)
    {
        if (m_maxValue == 0f)
        {
            return 0;
        }

        float floatValue = m_value.floatValue;
        amount = Mathf.Abs(amount);

        floatValue += amount;

        if (floatValue > m_maxValue)
        {
            float excess = floatValue - m_maxValue;
            return excess;
        }
        return 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>True if the value has changed</returns>
    public bool HasChanged()
    {
        return m_value.floatValue != m_prevValue;
    }

    /// <summary>
    /// Saves the value
    /// </summary>
    public void Commit()
    {
        m_prevValue = m_value.floatValue;
    }

    /// <summary>
    /// Gets the gap between the current Value and the old value
    /// </summary>
    /// <returns></returns>
    public float GetDifference()
    {
        return m_value.floatValue - m_prevValue;
    }

    /// <summary>
    /// Inspector Display
    /// </summary>
    public void PropertyField()
    {
        m_isOpen = EditorGUILayout.BeginToggleGroup(m_value.displayName, m_isOpen);
        // m_isLocked = EditorGUILayout.Toggle("Lock", m_isLocked);
        EditorGUILayout.PropertyField(m_value);
        EditorGUILayout.EndToggleGroup();
    }

    public void PropertyField(string label)
    {
        m_isOpen = EditorGUILayout.BeginToggleGroup(label, m_isOpen);
        // m_isLocked = EditorGUILayout.Toggle("Lock", m_isLocked);
        EditorGUILayout.PropertyField(m_value);
        EditorGUILayout.EndToggleGroup();
    }
}

/// <summary>
/// Makes sure that the total load is around Max
/// </summary>
public struct LoadShareManager
{
    public const float MAX = 100f;

    private Load[] m_members;

    public Load[] members
    {
        get
        {
            return m_members;
        }
    }

    public float MaxValue { get { return MAX; } }

    public int Count { get { return m_members.Length; } }

    
    public LoadShareManager(params Load[] members)
    {
        m_members = members;
    }

    public LoadShareManager(params SerializedProperty[] members)
    {
        m_members = new Load[members.Length];
        for (int i = 0; i < m_members.Length; i++)
        {
            m_members[i] = new Load(members[i]);
        }
    }

    /// <summary>
    /// Adds a new Member to the list
    /// </summary>
    /// <param name="newMember"></param>
    public void Add(Load newMember)
    {
        List<Load> list = new List<Load>(m_members);
        list.Add(newMember);
        m_members = list.ToArray();
    }

    ///// <summary>
    ///// Removes a member in the list, if the member has a load it will be redistributed
    ///// </summary>
    ///// <param name="member"></param>
    //public void Remove(Load member)
    //{

    //}

    /// <summary>
    /// Removes a member in the list, if the member has a load it will be redistributed
    /// </summary>
    /// <param name="index"></param>
    public void RemoveAt(int index)
    {
        m_members[index].ForceValue(0f);
        Share(index,true);
        List<Load> list = new List<Load>(m_members);
        list.RemoveAt(index);
        m_members = list.ToArray();
    }


    /// <summary>
    /// Makes sure that there are 
    /// </summary>
    public void CheckShares()
    {
        int OpenCount = 0;

        for (int i = 0; i < m_members.Length; i++)
        {
            if (!m_members[i].isLocked)
            {
                OpenCount++;
            }
        }

        if (OpenCount < 2)
        {
            for (int i = 0; i < m_members.Length; i++)
            {
                m_members[i].Revert();
            }
        }
    }

    /// <summary>
    /// Adjusts max value base on the number of locked members and their current load
    /// </summary>
    public void AdjustMaxValue()
    {
        float currentMax = MAX;

        for (int i = 0; i < m_members.Length; i++)
        {
            if (m_members[i].isLocked)
            {
                currentMax -= m_members[i].load;
            }
        }

        for (int i = 0; i < m_members.Length; i++)
        {
            m_members[i].SetMax(currentMax);
        }
    }

    /// <summary>
    /// Reverts all max value to the max,
    /// </summary>
    public void RevertMaxValue()
    {
        for (int i = 0; i < m_members.Length; i++)
        {
            m_members[i].SetMax(MAX);
        }
    }

    /// <summary>
    /// Shares the load to other members to maintain the total count
    /// If one changed
    /// </summary>
    public void ShareLoad()
    {
        for (int i = 0; i < m_members.Length; i++)
        {
            if (m_members[i].HasChanged())
            {
                Share(i);
                m_members[i].Commit();
                break;
            }
        }
    }

    /// <summary>
    /// Make the values of each member equal
    /// </summary>
    public void MakeEqual()
    {
        float distribution = 100f / m_members.Length;

        for (int i = 0; i < m_members.Length; i++)
        {
            m_members[i].ForceValue(distribution);
            m_members[i].Commit();
        }
    }

    /// <summary>
    /// Display property of member
    /// </summary>
    /// <param name="index">index of member</param>
    public void PropertyField(int index)
    {
        m_members[index].PropertyField();
    }

    /// <summary>
    /// Display property of member
    /// </summary>
    /// <param name="index">index of member</param>
    public void PropertyField(int index,string label)
    {
        m_members[index].PropertyField(label);
    }

    /// <summary>
    /// Gets the total load of all members
    /// </summary>
    /// <returns></returns>
    public float GetTotal()
    {
        float total = 0f;

        for (int i = 0; i < m_members.Length; i++)
        {
            total += m_members[i].load;
        }

        return total;
    }

    /// <summary>
    /// Gets the amount of members that are qualified to be deducted
    /// </summary>
    /// <param name="exceptionIndex">the index that will be skipped</param>
    /// <returns></returns>
    public int GetDeductQualifiedCount(int exceptionIndex)
    {
        int qualifiedCount = 0;
        for (int i = 0; i < m_members.Length; i++)
        {
            if (exceptionIndex != i)
            {
                if (m_members[i].canBeDeducted)
                {
                    qualifiedCount++;
                }
            }
        }

        return qualifiedCount;
    }

    /// <summary>
    /// Gets the amount of members that are qualified to be added
    /// </summary>
    /// <param name="exceptionIndex">the index that will be skipped</param>
    /// <returns></returns>
    public int GetAddQualifiedCount(int exceptionIndex)
    {
        int qualifiedCount = 0;
        for (int i = 0; i < m_members.Length; i++)
        {
            if (exceptionIndex != i)
            {
                if (m_members[i].canBeAdded)
                {
                    qualifiedCount++;
                }
            }
        }

        return qualifiedCount;
    }

    /// <summary>
    /// Deducts the load to qualified members
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="exceptionIndex"></param>
    /// <param name="force">deduct regardless if it is lock or not</param>
    public void DeductLoad(float amount, int exceptionIndex, bool force = false)
    {
        float excess = 0f;

        //Get smallest amount to deduct
        for (int i = 0; i < m_members.Length; i++)
        {
            if (exceptionIndex != i)
            {
                if (m_members[i].canBeDeducted)
                {
                    float testExcess = m_members[i].TryDeductLoad(amount);
                    excess = testExcess > excess ? testExcess : excess;
                }
            }
        }
        amount -= excess;

        //Deduct the load
        float qualifiedCount = GetDeductQualifiedCount(exceptionIndex);
        float distributedAmount = amount / qualifiedCount;


        for (int i = 0; i < m_members.Length; i++)
        {
            if (exceptionIndex != i)
            {
                if (m_members[i].canBeDeducted)
                {
                    m_members[i].DeductLoad(distributedAmount);
                }
            }
        }

        //Repeat if there are still more excess
        if (excess > 0f)
        {
            DeductLoad(excess, exceptionIndex,force);
        }
    }

    /// <summary>
    /// Add the load to qualified members
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="exceptionIndex"></param>
    /// <param name="force">deduct regardless if it is lock or not</param>
    public void AddLoad(float amount, int exceptionIndex, bool force = false)
    {
        float excess = 0f;

        //Get smallest amount to add
        for (int i = 0; i < m_members.Length; i++)
        {
            if (exceptionIndex != i)
            {
                if (m_members[i].canBeAdded)
                {
                    float testExcess = m_members[i].TryAddLoad(amount);
                    excess = testExcess > excess ? testExcess : excess;
                }
            }
        }
        amount -= excess;

        //Add the load
        float qualifiedCount = GetAddQualifiedCount(exceptionIndex);
        float distributedAmount = amount / qualifiedCount;

        for (int i = 0; i < m_members.Length; i++)
        {
            if (exceptionIndex != i)
            {
                if (m_members[i].canBeAdded)
                {
                    m_members[i].AddLoad(distributedAmount);
                }
            }
        }

        //Repeat if there are still more excess
        if (excess > 0f)
        {
            AddLoad(excess, exceptionIndex,force);
        }
    }

    /// <summary>
    /// Share the load to all qualified members
    /// </summary>
    /// <param name="index">index that will be skipped</param>
    public void Share(int index, bool force = false)
    {
        float difference = m_members[index].GetDifference();

        if (force)
        {
            RevertMaxValue();
        }

        if (difference > 0)
        {
            DeductLoad(-difference, index, force);
        }
        else if (difference < 0)
        {
            AddLoad(difference, index, force);
        }
    }

    /// <summary>
    /// Makes sure that all values are within their bounds
    /// </summary>
    public void BindValues()
    {
        for (int i = 0; i < m_members.Length; i++)
        {
            if (!m_members[i].isLocked)
            {
                m_members[i].BindValue();
            }
        }
    }

    /// <summary>
    /// A must to fully operate the Manager
    /// </summary>
    public void Operate()
    {
        CheckShares();
        AdjustMaxValue();
        BindValues();

        if (GUIUtility.hotControl != 0)
        {
            ShareLoad();
        }
    }
}
