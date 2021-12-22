using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownManager : MonoBehaviour
{
    [SerializeField] GameObject objectCanvas;
    [SerializeField] GameObject imageCanvas;
    [SerializeField] GameObject lensCanvas;
    [SerializeField] Slider slider;

    public bool callFromDropdown = false;
    public float maxOffset = 0.35f;


    public void OnDropdownValueChange(int targetPos)
    {
        callFromDropdown = true;
        if (targetPos == 0)
        {
            slider.minValue = slider.GetComponent<SliderDistanceManager>().initialMin;
            slider.maxValue = lensCanvas.transform.localPosition.z - 0.35f;
            slider.value = objectCanvas.transform.localPosition.z;
        }
        else if (targetPos == 1)
        {
            slider.minValue = objectCanvas.transform.localPosition.z + 0.35f;
            slider.maxValue = imageCanvas.transform.localPosition.z - 0.35f;
            slider.value = lensCanvas.transform.localPosition.z;
        }
        else
        {
            slider.minValue = lensCanvas.transform.localPosition.z + 0.35f;
            slider.maxValue = -slider.GetComponent<SliderDistanceManager>().initialMin - maxOffset;
            slider.value = imageCanvas.transform.localPosition.z;
        }
        StartCoroutine(resetIndicator());
    }

    IEnumerator resetIndicator()
    {
        yield return new WaitForSeconds(0.1f);
        callFromDropdown = false;
    }
}
