using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class LengthChanger : MonoBehaviour
{

    public Text text;
    public GameObject pendulum;
    public float curLength = 1.0f;

    // Use this for initialization
    void Start()
    {
        text.text = "1.0" + "M";
    }

    public void Changed(float value)
    {
        float newval = 0f;
        if (value >= 10)
        {
            newval = value / 20;
        }
        else
        {
            newval = 0.5f;
        }

        newval = (float) Math.Round((double)newval, 1);
        text.text = "" + newval.ToString("f1") + "M";

        value = value * -1;
        curLength = newval;

        pendulum.GetComponent<HingeJoint>().anchor = new Vector3(pendulum.GetComponent<HingeJoint>().connectedAnchor.x, newval, pendulum.GetComponent<HingeJoint>().connectedAnchor.z);

        if((pendulum.GetComponent<Rigidbody>().velocity.magnitude == 0) && (pendulum.transform.eulerAngles.x < 1.5 && pendulum.transform.eulerAngles.x > -1.5))
        {
            pendulum.GetComponent<Rigidbody>().MovePosition(new Vector3(0, (4.18f - newval + 1f), -2.956742e-08f));
            pendulum.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(0, 0, 0));
        }
    }
}