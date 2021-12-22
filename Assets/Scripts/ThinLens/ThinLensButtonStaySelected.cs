using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ThinLensButtonStaySelected : MonoBehaviour
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
        currentButton.GetComponent<ThinLensRecordDetailsManager>().setupDetails();
    }

    public void updateCurrentButton()
    {
        if (EventSystem.current.currentSelectedGameObject.CompareTag("LengthBtn"))
        {
            ColorBlock colors;
            if (currentButton != null)
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
        currentButton.GetComponent<ThinLensRecordDetailsManager>().setupEdit();
    }

    public void disableEdit()
    {
        currentButton.GetComponent<ThinLensRecordDetailsManager>().disableEdit();
    }

    public void editRecordField()
    {
        currentButton.GetComponent<ThinLensRecordDetailsManager>().editRecordField();
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
