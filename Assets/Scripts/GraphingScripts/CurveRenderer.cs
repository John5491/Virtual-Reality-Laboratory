using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(UILineRenderer))]
[RequireComponent(typeof(CanvasRenderer))]
public class CurveRenderer : MonoBehaviour
{
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    [SerializeField] Transform pointC;

    [SerializeField] bool useLineRenderer;
    [SerializeField] bool useUILineRenderer;
    [SerializeField] bool useAmateurLineRenderer;

    [SerializeField] GameObject linePrefab;

    public int noOfPoints = 10;

    LineRenderer lineRenderer;
    UILineRenderer uiLineRenderer;
    GameObject line;
    List<Vector3> lineRendererPoints;
    List<Vector3> currentLineRendererPoints;
    List<Vector2> uiLineRendererPoints;
    List<Vector2> currentUILineRendererPoints;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        uiLineRenderer = GetComponent<UILineRenderer>();
    }

    private void Update()
    {
        _ = useLineRenderer ? lineRenderer.enabled = true : lineRenderer.enabled = false;
        _ = useUILineRenderer ? uiLineRenderer.enabled = true : uiLineRenderer.enabled = false;

        GetAllPointsOnLineRendererCurve();
        GetAllPointsOnUILineRendererCurve();
        if(currentLineRendererPoints != lineRendererPoints && useLineRenderer)
        {
            SetLineRendererPoints();
        }
        if(currentUILineRendererPoints != uiLineRendererPoints && useUILineRenderer)
        {
            SetUILineRendererPoints();
            uiLineRenderer.SetAllDirty();
        }
        if(useAmateurLineRenderer)
        {
            DrawAmateurLineRenderer();
        }
    }

    private void SetLineRendererPoints()
    {
        lineRenderer.positionCount = lineRendererPoints.Count;
        lineRenderer.SetPositions(lineRendererPoints.ToArray());
        currentLineRendererPoints = lineRendererPoints;
    }

    private void SetUILineRendererPoints()
    {
        uiLineRenderer.points = new List<Vector2>();
        uiLineRenderer.points = uiLineRendererPoints;
        currentUILineRendererPoints = uiLineRendererPoints;
    }

    private void DrawAmateurLineRenderer()
    {
        GameObject[] lines = GameObject.FindGameObjectsWithTag("Line");

        foreach(GameObject line in lines)
        {
            Destroy(line);
        }

        for(int i = 0; i < uiLineRendererPoints.Count - 1; i++)
        {
            line = Instantiate(linePrefab, transform.position, Quaternion.identity, transform);
            RectTransform rectTransform = line.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = uiLineRendererPoints[i];
            rectTransform.localEulerAngles = Vector3.zero;
            Vector2 origin = uiLineRendererPoints[i];
            
            Vector2 direction = uiLineRendererPoints[i + 1] - origin;
            line.transform.right = direction;

            line.transform.localScale = new Vector3(direction.magnitude, 1, 1);
        }
    }

    private void GetAllPointsOnLineRendererCurve()
    {
        lineRendererPoints = new List<Vector3>();
        float positionZ = pointA.position.z;
        float distance = GetDistance(pointA.position, pointC.position);
        float gap = distance / noOfPoints;
        for (int i = 0; i < noOfPoints; i++)
        {
            Vector3 position = QuadraticCurve(pointA.position, pointB.position, pointC.position, i * gap / distance);
            lineRendererPoints.Add(position);
        }
        lineRendererPoints.Add(pointC.position);
    }

    private void GetAllPointsOnUILineRendererCurve()
    {
        uiLineRendererPoints = new List<Vector2>();
        float distance = GetDistance(pointA.localPosition, pointC.localPosition);
        float gap = distance / noOfPoints;
        for (int i = 0; i < noOfPoints; i++)
        {
            Vector2 position = QuadraticCurve(pointA.localPosition, pointB.localPosition, pointC.localPosition, i * gap / distance);
            uiLineRendererPoints.Add(position);
        }
        uiLineRendererPoints.Add(pointC.localPosition);
    }

    private float GetDistance(Vector3 a, Vector3 b)
    {
        return Mathf.Sqrt(Mathf.Pow(b.x - a.x, 2) + Mathf.Pow(b.y - a.y, 2));
    }

    private Vector3 QuadraticCurve(Vector3 a, Vector3 b, Vector3 c, float x)
    {
        Vector3 p0 = Vector3.Lerp(a, b, x);
        Vector3 p1 = Vector3.Lerp(b, c, x);
        return Vector3.Lerp(p0, p1, x);
    }
}
