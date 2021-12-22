using UnityEngine;
using System.Collections;
using System.Text;
using UnityEngine.UI;
using TMPro;

public class PendulumBehaviour : MonoBehaviour
{
    public Text pendulumText;
    public Transform pivot;
    private float curangle;
    public Rigidbody rigid;
    public Text canvasText;
    public GameObject Timer;

    public bool enable = false;

    bool mouseDown = false;

    void Update()
    {
        curangle = pivot.transform.rotation.eulerAngles.x;

        if(!mouseDown)
        {
            if (curangle > 0.5)
            {
                CalculateAngle();
                pendulumText.text = " : " + curangle.ToString("f1");
                canvasText.text = curangle.ToString("f1");
            }
        }
    }

    public void setEnable(bool variable)
    {
        enable = variable;
    }

    void CalculateAngle()
    {
        if (curangle > 90 && curangle < 180)
        {
            curangle = 90 - curangle;
        }
        else if (curangle > 180 && curangle < 270)
        {
            curangle = 180 - curangle;
        }
        else if (curangle > 270 && curangle <= 360)
        {
            curangle = 360 - curangle;
        }
    }

    void OnMouseDown()
    {
        if(enable)
        {
            mouseDown = true;
            Timer.GetComponent<StopWatch>().StopStopwatch();
            pendulumText.text = " : ";
            canvasText.text = "";
        }
    }

    private void OnMouseUp()
    {
        if (enable)
        {
            Timer.GetComponent<StopWatch>().currentTime = 0;
            Timer.GetComponent<StopWatch>().StartStopwatch();
            mouseDown = false;
        }
    }
}