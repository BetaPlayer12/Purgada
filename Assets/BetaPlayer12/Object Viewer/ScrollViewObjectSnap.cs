using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectViewer))]
public class ScrollViewObjectSnap : MonoBehaviour
{
    public enum Direction
    {
        Horizontal,
        Vertical,
        _Count
    }

    [Header("References")]
    [SerializeField]
    protected RectTransform m_centerImage;
    [SerializeField]
    protected RectTransform m_scrollViewer;

    [Header("Scroll Settings")]
    [SerializeField]
    protected Direction m_scrollDirection;
    [SerializeField]
    protected float m_lerpSpeed;

    private List<RectTransform> m_list = null;
    private float[] m_panelDistanceReposition;
    [SerializeField]
    protected float[] m_panelDistance;
    [SerializeField]
    protected int m_closestIndex = -1;
    private int m_prevClosestIndex = -1;

    public float CenterImageYPos { get { return m_centerImage.position.y; } }
    public float CenterImageXPos { get { return m_centerImage.position.x; } }
    public int PrevClosetIndex { set { m_prevClosestIndex = value; } }
    public GameObject chosenGO { get { return list[m_closestIndex].gameObject; } }

    protected float[] panelDistance { get { return m_panelDistance; } }
    protected int closestIndex { get { return m_closestIndex; } }

    protected List<RectTransform> list { get { return m_list; } }

    public void OnDrag()
    {
        StopAllCoroutines();
        ComputeDistance();
    }

    public void OnEndDrag()
    {
        var minDistance = Mathf.Min(m_panelDistance);
        for (int a = 0; a < m_panelDistance.Length; a++)
        {
            if (minDistance == m_panelDistance[a])
            {
                m_closestIndex = a;
                StartCoroutine(SnapPanel(m_closestIndex));
                return;
            }
        }
    }

    private float GetObjectPositionValue(int index)
    {
        return m_scrollDirection == Direction.Horizontal ? list[index].GetComponent<RectTransform>().position.x : list[index].GetComponent<RectTransform>().position.y;
    }
    private float GetObjectPositionValue(RectTransform rectTrans, bool anchorPosition = false)
    {

        var position = anchorPosition ? rectTrans.anchoredPosition : (Vector2)rectTrans.position;
        return m_scrollDirection == Direction.Horizontal ? position.x : position.y;
    }

    protected void ComputeDistance() {
        for (int i = 0; i < list.Count; i++)
        {
            m_panelDistanceReposition[i] = GetObjectPositionValue(m_centerImage) - GetObjectPositionValue(i);
            m_panelDistance[i] = Mathf.Abs(m_panelDistanceReposition[i]);
        }
    }

    private IEnumerator SnapPanel(int index)
    {
        var anchoredPosition = list[index].GetComponent<RectTransform>().anchoredPosition;
        var position = m_scrollDirection == Direction.Vertical ? -anchoredPosition.y : -anchoredPosition.x;
        var viewerPosition = GetObjectPositionValue(m_scrollViewer, true);
        while (viewerPosition != position)
        {
            float value = Mathf.Lerp(viewerPosition, position, Time.deltaTime * m_lerpSpeed);
            Vector2 newPosition = m_scrollDirection == Direction.Horizontal ? new Vector2(value, m_scrollViewer.anchoredPosition.y) : new Vector2(m_scrollViewer.anchoredPosition.x, value);
            m_scrollViewer.anchoredPosition = newPosition;
            viewerPosition = GetObjectPositionValue(m_scrollViewer, true);
            yield return null;
        }
    }

    protected void StartSnapPanel(int index) {
        StopAllCoroutines();
        StartCoroutine(SnapPanel(index));
    }

    protected virtual void OnStartModule() { }

    void Start()
    {
        if (m_list == null)
        {
            m_list = new List<RectTransform>();
            var GOList = GetComponent<ObjectViewer>().GOList;

            foreach (GameObject GO in GOList)
                m_list.Add(GO.GetComponent<RectTransform>());

            m_panelDistance = new float[m_list.Count];
            m_panelDistanceReposition = new float[m_list.Count];
        }
        ComputeDistance();
        OnStartModule();
    }
}
