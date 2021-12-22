using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ThinLensRecordDetailsManager : MonoBehaviour
{
    [SerializeField] GameObject controlPanel;
    [SerializeField] GameObject tableContent1;
    [SerializeField] GameObject detailsPanelHolder;
    [SerializeField] GameObject detailsPanel;
    [SerializeField] GameObject recordButtonPanel;
    [SerializeField] Text objectDistanceText;
    [SerializeField] Text imageDistanceText;
    [SerializeField] Text inverseObjectDistanceText;
    [SerializeField] Text inverseImageDistanceText;
    [SerializeField] Text focalLengthText;
    [SerializeField] InputField imageDistanceInputField;
    [SerializeField] InputField inverseObjectDistanceInputField;
    [SerializeField] InputField inverseImageDistanceInputField;
    [SerializeField] InputField focalLengthInputField;

    List<ThinLensRecord> records;
    public float objectDistance;

    public void setupDetails()
    {
        records = controlPanel.GetComponent<ThinLensSaveRecord>().GetRecordList();
        int positionToUpdate = records.FindIndex(x => x.getObjectDistance() == objectDistance);

        objectDistanceText.text = records[positionToUpdate].getObjectDistance().ToString("f3");
        imageDistanceText.text = records[positionToUpdate].getImageDistance().ToString("f3");
        inverseObjectDistanceText.text = records[positionToUpdate].getInverseObjectDistance().ToString("f3");
        inverseImageDistanceText.text = records[positionToUpdate].getInverseImageDistance().ToString("f3");
        focalLengthText.text = records[positionToUpdate].getFocalLength().ToString("f3");
        
        imageDistanceInputField.text = imageDistanceText.text;
        inverseObjectDistanceInputField.text = inverseObjectDistanceText.text;
        inverseImageDistanceInputField.text = inverseImageDistanceText.text;
        focalLengthInputField.text = focalLengthText.text;

        detailsPanel.SetActive(true);
        if (!tableContent1.GetComponent<ThinLensRecordButtonGenerator>().faded)
        {
            StartCoroutine(fadeIn(detailsPanel.GetComponent<CanvasGroup>(), 0.0f, 1.0f));
            recordButtonPanel.GetComponent<Animator>().SetTrigger("slide");
            detailsPanelHolder.GetComponent<Animator>().SetTrigger("slide");
            tableContent1.GetComponent<ThinLensRecordButtonGenerator>().faded = true;
        }
        else
        {
            detailsPanel.GetComponent<CanvasGroup>().alpha = 1.0f;
        }
    }

    public void setupEdit()
    {
        imageDistanceInputField.gameObject.SetActive(true);
        inverseObjectDistanceInputField.gameObject.SetActive(true);
        inverseImageDistanceInputField.gameObject.SetActive(true);
        focalLengthInputField.gameObject.SetActive(true);
    }

    public void disableEdit()
    {
        setupDetails();
        imageDistanceInputField.gameObject.SetActive(false);
        inverseObjectDistanceInputField.gameObject.SetActive(false);
        inverseImageDistanceInputField.gameObject.SetActive(false);
        focalLengthInputField.gameObject.SetActive(false);
    }

    public void editRecordField()
    {
        records = controlPanel.GetComponent<ThinLensSaveRecord>().GetRecordList();
        int positionToUpdate = records.FindIndex(x => x.getObjectDistance() == objectDistance);
        Debug.Log(records[positionToUpdate].getInverseImageDistance());
        records[positionToUpdate].UpdateVariables(
            float.Parse(imageDistanceInputField.text), 
            float.Parse(focalLengthInputField.text),
            float.Parse(inverseObjectDistanceInputField.text), 
            float.Parse(inverseImageDistanceInputField.text));
        setupDetails();
        Debug.Log(records[positionToUpdate].getInverseImageDistance());
        disableEdit();
    }

    private IEnumerator fadeIn(CanvasGroup canvasGroup, float start, float end)
    {
        yield return new WaitForSeconds(1.0f);
        float counter = 0;
        while (counter < 0.5f)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, counter / 0.5f);

            yield return null;
        }
    }
}
