using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovementSlide : MonoBehaviour
{
    [SerializeField] GameObject distanceCamera;
    [SerializeField] GameObject leftPanel;
    [SerializeField] GameObject rightPanel;
    [SerializeField] float speed = 2f;
    [SerializeField] float buttonAlphaColor = 0.5f;
    [SerializeField] float cameraSlideMax = 15.0f;
    [SerializeField] float cameraSlideMaxOffset = 2.0f;

    bool left = false;
    bool right = false;

    private void Update()
    {
        if(left)
        {
            cameraSlideLeft();
        }
        if(right)
        {
            cameraSlideRight();
        }
    }

    public void setLeft()
    {
        left = !left;
        var color = leftPanel.GetComponent<Image>().color;
        if (left)
            color.a = buttonAlphaColor;
        else
            color.a = 0f;
        leftPanel.GetComponent<Image>().color = color;
    }

    public void setRight()
    {
        right = !right;
        var color = rightPanel.GetComponent<Image>().color;
        if (right)
            color.a = buttonAlphaColor;
        else
            color.a = 0f;
        rightPanel.GetComponent<Image>().color = color;
    }

    private void cameraSlideLeft()
    {
        if(distanceCamera.transform.localPosition.x > -cameraSlideMax)
            distanceCamera.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
    }

    public void jumpLeft()
    {
        if (distanceCamera.transform.localPosition.x > -cameraSlideMax)
            distanceCamera.transform.position -= new Vector3(2, 0, 0);
    }

    public void colorChangeOnButtonDown(GameObject button)
    {
        var color = button.GetComponent<Image>().color;
        color.a = buttonAlphaColor * 2;
        button.GetComponent<Image>().color = color;
    }

    public void colorChangeOnButtonUp(GameObject button)
    {
        var color = button.GetComponent<Image>().color;
        color.a = 0.11f;
        button.GetComponent<Image>().color = color;
    }

    public void jumpRight()
    {
        if (distanceCamera.transform.localPosition.x < cameraSlideMax + cameraSlideMaxOffset)
            distanceCamera.transform.position += new Vector3(2, 0, 0);
    }

    private void cameraSlideRight()
    {
        if (distanceCamera.transform.localPosition.x < cameraSlideMax + cameraSlideMaxOffset)
            distanceCamera.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
    }
}
