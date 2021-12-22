using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RecordDetails : MonoBehaviour
{
    [SerializeField] GameObject saveBtn;
    [SerializeField] GameObject tableContent1;
    [SerializeField] GameObject detailsPanelHolder;
    [SerializeField] GameObject detailsPanel;
    [SerializeField] GameObject lengthPanel;
    [SerializeField] Text record1;
    [SerializeField] Text record2;
    [SerializeField] Text record3;
    [SerializeField] Text average;
    [SerializeField] Text averageInSec;
    [SerializeField] Text frequency;
    [SerializeField] Text period;
    [SerializeField] Text periodSquare;
    [SerializeField] InputField avgInSecInptFld;
    [SerializeField] InputField periodInptFld;
    [SerializeField] InputField periodSInptFld;

    List<Records> table;
    public float length;

    public void setupDetails()
    {
        table = saveBtn.GetComponent<Table>().table;
        int positionToUpdate = table.FindIndex(x => x.length == length);
        record1.text = (table[positionToUpdate].timeRecord[0] > 0.0f) ? TimeSpan.FromSeconds(table[positionToUpdate].timeRecord[0]).ToString(@"mm\:ss\:ff") : "-";
        record2.text = (table[positionToUpdate].timeRecord[1] > 0.0f) ? TimeSpan.FromSeconds(table[positionToUpdate].timeRecord[1]).ToString(@"mm\:ss\:ff") : "-";
        record3.text = (table[positionToUpdate].timeRecord[2] > 0.0f) ? TimeSpan.FromSeconds(table[positionToUpdate].timeRecord[2]).ToString(@"mm\:ss\:ff") : "-";
        average.text = (table[positionToUpdate].average > 0.0f) ? TimeSpan.FromSeconds(table[positionToUpdate].average).ToString(@"mm\:ss\:ff") : "-";
        averageInSec.text = table[positionToUpdate].average.ToString("f1");
        frequency.text = table[positionToUpdate].frequency + "Hz";
        period.text = table[positionToUpdate].period.ToString("f1");
        periodSquare.text = table[positionToUpdate].periodSquare.ToString("f1");
        avgInSecInptFld.text = table[positionToUpdate].average.ToString("f1");
        periodInptFld.text = table[positionToUpdate].period.ToString("f1");
        periodSInptFld.text = table[positionToUpdate].periodSquare.ToString("f1");

        detailsPanel.SetActive(true);
        if (!tableContent1.GetComponent<RecordButton>().faded)
        {
            StartCoroutine(fadeIn(detailsPanel.GetComponent<CanvasGroup>(), 0.0f, 1.0f));
            lengthPanel.GetComponent<Animator>().SetTrigger("slide");
            detailsPanelHolder.GetComponent<Animator>().SetTrigger("slide");
            tableContent1.GetComponent<RecordButton>().faded = true;
        }
        else
        {
            detailsPanel.GetComponent<CanvasGroup>().alpha = 1.0f;
        }
    }

    public void setupEdit()
    {
        avgInSecInptFld.gameObject.SetActive(true);
        periodInptFld.gameObject.SetActive(true);
        periodSInptFld.gameObject.SetActive(true);
    }

    public void disableEdit()
    {
        setupDetails();
        avgInSecInptFld.gameObject.SetActive(false);
        periodInptFld.gameObject.SetActive(false);
        periodSInptFld.gameObject.SetActive(false);
    }

    public void editRecordField()
    {
        table = saveBtn.GetComponent<Table>().table;
        int positionToUpdate = table.FindIndex(x => x.length == length);
        table[positionToUpdate].updateVariables(float.Parse(avgInSecInptFld.text), float.Parse(periodInptFld.text), float.Parse(periodSInptFld.text));
        setupDetails();
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
