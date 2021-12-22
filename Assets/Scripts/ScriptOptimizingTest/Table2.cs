using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class Table2 : MonoBehaviour
{
    [SerializeField] GameObject pendulum;
    [SerializeField] GameObject Timer;
    [SerializeField] GameObject slider;
    [SerializeField] GameObject controlPanel;
    [SerializeField] GameObject savedSign;
    [SerializeField] GameObject recordSavedOption;
    [SerializeField] GameObject replaceOption;
    [SerializeField] GameObject canceledSign;
    [SerializeField] GameObject resetBtn;
    [SerializeField] float duration = 0.4f;

    [SerializeField] string xName = "Length";
    [SerializeField] RecordsHandler recordsHandler;
    public List<Records2> records;

    // Start is called before the first frame update
    void Start()
    {
        records = recordsHandler.Records;
    }

    public void saveRecord()
    {
        pendulum.GetComponent<PendulumReposition>().enable = false;
        pendulum.GetComponent<PendulumBehaviour>().enable = false;
        controlPanel.GetComponent<CanvasGroup>().interactable = false;

        float curLength = slider.GetComponent<LengthChanger>().curLength;
        int positionToUpdate = records.FindIndex(x => x.RecordColumn[recordsHandler.GetColumnIndex(xName)] == (object) curLength);
        if (positionToUpdate >= 0)
        {
            if ((int) records[positionToUpdate].RecordColumn[recordsHandler.GetColumnIndex("curPosition")] < 3)
            {
                recordSavedOption.SetActive(true);
            }
            else
            {
                replaceOption.SetActive(true);
            }
        }
        else
        {
            addRecord();
        }
    }

    public void addRecord()
    {
        float curLength = slider.GetComponent<LengthChanger>().curLength;
        recordsHandler.AddToRecords(new Records2(new List<object>() { curLength, Timer.GetComponent<StopWatch>().currentTime, "", "", "", 10, "", "", "", 2 }));
        GetComponent<Button>().interactable = false;
        savedSign.SetActive(true);

        StartCoroutine(DoFade(savedSign.GetComponent<CanvasGroup>(), 1f, 0f, savedSign, false));
    }

    public void addColumn()
    {
        float curLength = slider.GetComponent<LengthChanger>().curLength;
        int positionToUpdate = records.FindIndex(x => x.RecordColumn[recordsHandler.GetColumnIndex(xName)] == (object)curLength);
        records[positionToUpdate].SetElement((int) records[positionToUpdate].GetElement(recordsHandler.GetColumnIndex("curPosition")), Timer.GetComponent<StopWatch>().currentTime);
        GetComponent<Button>().interactable = false;
        savedSign.SetActive(true);

        StartCoroutine(DoFade(savedSign.GetComponent<CanvasGroup>(), 1f, 0f, savedSign, false));
    }

    public void cancelSave()
    {
        canceledSign.SetActive(true);
        StartCoroutine(DoFade(canceledSign.GetComponent<CanvasGroup>(), 1f, 0f, canceledSign, true));
    }

    public void updateBtn(int position)
    {
        float curLength = slider.GetComponent<LengthChanger>().curLength;
        int positionToUpdate = records.FindIndex(x => x.RecordColumn[recordsHandler.GetColumnIndex(xName)] == (object)curLength);
        records[positionToUpdate].SetElement(position + 1, Timer.GetComponent<StopWatch>().currentTime);
        GetComponent<Button>().interactable = false;
        savedSign.SetActive(true);

        StartCoroutine(DoFade(savedSign.GetComponent<CanvasGroup>(), 1f, 0f, savedSign, false));
    }

    private IEnumerator DoFade(CanvasGroup canvasGroup, float start, float end, GameObject gameCanvas, bool isCancel)
    {
        yield return new WaitForSeconds(0.5f);
        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, counter / duration);

            yield return null;
        }

        if (canvasGroup.alpha == 0f)
        {
            if (!isCancel)
            {
                gameCanvas.SetActive(false);
                gameCanvas.GetComponent<CanvasGroup>().alpha = 1.0f;
                resetBtn.GetComponent<ResetScript>().reset();
                resetBtn.GetComponent<Button>().interactable = false;
            }

            pendulum.GetComponent<PendulumReposition>().enable = true;
            pendulum.GetComponent<PendulumBehaviour>().enable = true;
            controlPanel.GetComponent<CanvasGroup>().interactable = true;
        }
    }
}
