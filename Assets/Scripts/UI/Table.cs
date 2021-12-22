using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class Table : MonoBehaviour
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
    public List<Records> table;

    // Start is called before the first frame update
    void Start()
    {
        table = new List<Records>();
    }

    public void saveRecord()
    {
        pendulum.GetComponent<PendulumReposition>().enable = false;
        pendulum.GetComponent<PendulumBehaviour>().enable = false;
        controlPanel.GetComponent<CanvasGroup>().interactable = false;

        float curLength = slider.GetComponent<LengthChanger>().curLength;
        int positionToUpdate = table.FindIndex(x => x.length == curLength);
        if (positionToUpdate >= 0)
        {
            if(table[positionToUpdate].curPosition < 3)
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
        table.Add(new Records(curLength, Timer.GetComponent<StopWatch>().currentTime));
        GetComponent<Button>().interactable = false;
        savedSign.SetActive(true);

        StartCoroutine(DoFade(savedSign.GetComponent<CanvasGroup>(), 1f, 0f, savedSign, false));
    }

    public void addColumn()
    {
        float curLength = slider.GetComponent<LengthChanger>().curLength;
        int positionToUpdate = table.FindIndex(x => x.length == curLength);
        table[positionToUpdate].addRecord(Timer.GetComponent<StopWatch>().currentTime);
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
        int positionToUpdate = table.FindIndex(x => x.length == curLength);
        table[positionToUpdate].changeRecord(Timer.GetComponent<StopWatch>().currentTime, position);
        GetComponent<Button>().interactable = false;
        savedSign.SetActive(true);

        StartCoroutine(DoFade(savedSign.GetComponent<CanvasGroup>(), 1f, 0f, savedSign, false));
    }

    private IEnumerator DoFade(CanvasGroup canvasGroup, float start, float end, GameObject gameCanvas, bool isCancel)
    {
        yield return new WaitForSeconds(0.5f);
        float counter = 0;
        while(counter < duration)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, counter / duration);

            yield return null;
        }
        
        if (canvasGroup.alpha == 0f)
        {
            if(!isCancel)
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
