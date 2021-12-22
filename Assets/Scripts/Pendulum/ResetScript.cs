using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResetScript : MonoBehaviour
{

    public Rigidbody pivot;
    public GameObject slider;
    public float curangle;
    public float resetangle;
    public Text pendulumText;
    public Text canvasText;
    public GameObject Timer;

    public void reset()
    {
        pivot.isKinematic = true;
        pivot.MovePosition(new Vector3(0, 4.18f - (slider.GetComponent<LengthChanger>().curLength - 1f), -2.956742e-08f));
        pivot.MoveRotation(Quaternion.Euler(0, 0, 0));

        Timer.GetComponent<StopWatch>().currentTime = 0;
        StartCoroutine(resetText());
    }

    IEnumerator resetText()
    {
        yield return new WaitForSeconds(0.25f);
        pendulumText.text = " : 0.0";
        canvasText.text = "0.0";
    }
}