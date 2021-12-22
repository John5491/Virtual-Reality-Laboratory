using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GraphManager : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] GameObject saveBtn;
    [SerializeField] GameObject graphBackground;
    [SerializeField] RectTransform graphContainer;
    [SerializeField] GameObject linePrefab;
    [SerializeField] GameObject curvePrefab;


    [SerializeField] private Sprite circleSprite;
    
    public int plotMode = 0;
    GameObject line;
    Vector2 origin;

    bool curvehideHandles = false;

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.H))
        {
            UpdateHandles();
        }
    }

    private void UpdateHandles()
    {
        curvehideHandles = !curvehideHandles;
        GameObject[] handles = GameObject.FindGameObjectsWithTag("CurveHandles");
        foreach(GameObject handle in handles)
        {
            Color color = handle.GetComponent<Image>().color;
            color.a = curvehideHandles ? .0f : 1.0f;
            handle.GetComponent<Image>().color = color;
        }
    }

    public void SetPlotMode(int mode)
    {
        plotMode = mode;
    }

    public void ClearGraph()
    {
        foreach(Transform child in transform)
        {
            if(child.name != ("Grid"))
            {
                Destroy(child.gameObject);
            }
        }
    }

    public void OnPointerClick(PointerEventData ped)
    {
        if (plotMode == 0)
        {
            Vector2 localCursor;
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), ped.position, ped.pressEventCamera, out localCursor))
                return;

            CreateCircle(localCursor);
        }
        if(plotMode == 2)
        {
            if(ped.button == PointerEventData.InputButton.Left && Input.GetKey(KeyCode.LeftShift))
            {
                GameObject curve = Instantiate(curvePrefab, transform);
            }
        }
    }

    public void OnPointerDown(PointerEventData ped)
    {
        if (plotMode == 1)
        {
            Vector2 localCursor;
            line = Instantiate(linePrefab, transform.position, Quaternion.identity, transform);
            line.AddComponent<OnClickDelete>();
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), ped.position, ped.pressEventCamera, out localCursor))
                return;

            RectTransform rectTransform = line.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = localCursor;
            rectTransform.localEulerAngles = Vector3.zero;
            origin = ped.position;
        }
    }

    public void OnDrag(PointerEventData ped)
    {
        if (plotMode == 1)
        {
            UpdateLine(ped.position);
        }
    }

    public void OnPointerUp(PointerEventData ped)
    {
        if (plotMode == 1)
        {
            UpdateLine(ped.position);
        }
    }

    private void CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("cirlce", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        gameObject.AddComponent<OnClickDelete>();
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
    }

    private void UpdateLine(Vector3 position)
    {
        Vector3 temp = new Vector3(origin.x, origin.y, 0);
        Vector3 direction = position - temp;
        line.transform.right = direction;

        line.transform.localScale = new Vector3(direction.magnitude, 1, 1);
    }
}
