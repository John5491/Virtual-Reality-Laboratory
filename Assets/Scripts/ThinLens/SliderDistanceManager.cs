using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderDistanceManager : MonoBehaviour
{
    [SerializeField] GameObject objectCanvas;
    [SerializeField] GameObject imageCanvas;
    [SerializeField] GameObject lensCanvas;
    [SerializeField] Dropdown dropdown;
    [SerializeField] float plusMinusDistance = 0.001f;

    public float initialMin = 8.45f;

    private void Start()
    {
        gameObject.GetComponent<Slider>().value = objectCanvas.transform.localPosition.z;
        gameObject.GetComponent<Slider>().minValue = initialMin;
        gameObject.GetComponent<Slider>().maxValue = lensCanvas.transform.localPosition.z - 0.35f;

    }

    public void OnSliderValueChange(float value)
    {
        if (!dropdown.gameObject.GetComponent<DropdownManager>().callFromDropdown)
        {
            Vector3 newPos;

            if (dropdown.value == 0)
            {
                newPos = objectCanvas.transform.localPosition;
                newPos.z = value;
                objectCanvas.transform.localPosition = newPos;
            }
            else if (dropdown.value == 1)
            {
                newPos = lensCanvas.transform.localPosition;
                newPos.z = value;
                lensCanvas.transform.localPosition = newPos;
            }
            else
            {
                newPos = imageCanvas.transform.localPosition;
                newPos.z = value;
                imageCanvas.transform.localPosition = newPos;
            }
        }
    }

    public void MinusDistance()
    {
        gameObject.GetComponent<Slider>().value -= plusMinusDistance;
    }

    public void PlusDistance()
    {
        gameObject.GetComponent<Slider>().value += plusMinusDistance;
    }
}
