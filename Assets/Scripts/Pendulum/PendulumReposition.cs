using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumReposition : MonoBehaviour {
    [SerializeField] float maxPower = 5.0f;
    [SerializeField] float minPower = 1.0f;
    [SerializeField] Transform anchorTransf;
    [SerializeField] GameObject pendulumTransf;

    public GameObject anchor;
    public GameObject traceAngle;
    public GameObject slider;
    public bool enable = false;

    Plane anchorControlPlane = new Plane();
    Camera cam;

    void Awake() {
        cam = Camera.main;
        StartCoroutine(positionStartUp());
    }

    public void setEnable(bool variable)
    {
        enable = variable;
    }

    void OnMouseDown() {
        if(enable)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            UpdateAnchorControlPlane();
            UpdatePendulumPosition();
        }
    }

    void OnMouseDrag() {
        if(enable)
        {
            UpdatePendulumPosition();
        }
    }

    private void OnMouseUp()
    {
        if(enable)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    void UpdateAnchorControlPlane() {
        Vector3 anchorPos = anchorTransf.position;
        anchorControlPlane.Set3Points(anchorPos + Vector3.forward, anchorPos + Vector3.up, anchorPos + Vector3.back);
    }

    void UpdatePendulumPosition() {
        maxPower = slider.GetComponent<LengthChanger>().curLength;
        minPower = maxPower;

        Vector3 anchorPosition = anchorTransf.position;
        Vector3 posDifference = GetPlanePoint(Input.mousePosition) - anchorPosition;
        posDifference = ClampToMagnitude(posDifference, minPower, maxPower);
        
        pendulumTransf.GetComponent<Rigidbody>().MovePosition(anchorPosition + posDifference);
        lookAt();
    }

    Vector3 GetPlanePoint(Vector3 pos) {
        float distance;

        Ray ray = cam.ScreenPointToRay(pos);
        if(anchorControlPlane.Raycast(ray, out distance)) {
            return ray.GetPoint(distance);
        }

        return pos;
    }
    
    Vector3 ClampToMagnitude(Vector3 vec, float min, float max) {
        return vec.normalized * Mathf.Clamp(vec.magnitude, min, max);
    }

    void lookAt()
    {
        var localTarget = transform.InverseTransformPoint(anchor.transform.position);
        float angle = traceAngle.transform.eulerAngles.x - 90f;
        if (transform.position.z > 0) angle *= -1;
        Vector3 eulerAngleVelocity = new Vector3(angle, 0, 0);
        Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity);
        pendulumTransf.GetComponent<Rigidbody>().MoveRotation(Quaternion.AngleAxis(angle, Vector3.left));
    }

    IEnumerator positionStartUp()
    {
        yield return new WaitForSeconds(0.1f);
        pendulumTransf.GetComponent<Rigidbody>().isKinematic = true;
    }
}
