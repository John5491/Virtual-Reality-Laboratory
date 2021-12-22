using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonStaySelected : MonoBehaviour
{
    [SerializeField] Button sampleLengthButton;
    private Button currentButton;
    private Color originalNormalColor;
    private Color selectedColor;

    private void Awake()
    {
        originalNormalColor = sampleLengthButton.colors.normalColor;
        selectedColor = sampleLengthButton.colors.selectedColor;
    }

    public void updateDetailsWithCurrentButton()
    {
        currentButton.GetComponent<RecordDetails>().setupDetails();
    }

    public void updateCurrentButton()
    {
        if(EventSystem.current.currentSelectedGameObject.CompareTag("LengthBtn"))
        {
            ColorBlock colors;
            if(currentButton != null)
            {
                colors = sampleLengthButton.colors;
                colors.normalColor = originalNormalColor;
                currentButton.colors = colors;
            }
            currentButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
            colors = sampleLengthButton.colors;
            colors.normalColor = selectedColor;
            currentButton.colors = colors;
        }
    }

    public void setupEdit()
    {
        currentButton.GetComponent<RecordDetails>().setupEdit();
    }

    public void disableEdit()
    {
        currentButton.GetComponent<RecordDetails>().disableEdit();
    }

    public void editRecordField()
    {
        currentButton.GetComponent<RecordDetails>().editRecordField();
    }

    public void resetCurrentButton()
    {
        if (currentButton != null)
        {
            var colors = sampleLengthButton.colors;
            colors.normalColor = originalNormalColor;
            currentButton.colors = colors;
        }
        currentButton = null;
    }
}
