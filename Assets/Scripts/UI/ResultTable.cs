using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class ResultTable : MonoBehaviour
{
    [SerializeField] Transform entryContainer;
    [SerializeField] Transform entryTemplate;
    [SerializeField] float templateHeight = 100f;
    [SerializeField] GameObject saveBtn;


    List<Records> table;
    private List<Transform> recordEntryTransfromList;

    private void OnEnable()
    {
        entryTemplate.gameObject.SetActive(false);
        table = saveBtn.GetComponent<Table>().table;

        sortTable();
        recordEntryTransfromList = new List<Transform>();
        
        foreach (Records item in table)
        {
            CreateRecordInTable(item, entryContainer, recordEntryTransfromList);
        }
    }
    
    private void sortTable()
    {
        for (int i = 0; i < table.Count; i++)
        {
            for (int j = i + 1; j < table.Count; j++)
            {
                if (table[j].length < table[i].length)
                {
                    Records tmp = table[i];
                    table[i] = table[j];
                    table[j] = tmp;
                }
            }
        }
    }

    private void CreateRecordInTable(Records recordEntry, Transform container, List<Transform> transformList)
    {
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);
        
        entryTransform.Find("LengthRecord").GetComponent<Text>().text = recordEntry.length.ToString();
        entryTransform.Find("RecordAverage").GetComponent<Text>().text = (recordEntry.average > 0.0f) ? recordEntry.average.ToString("f2") : "-";
        entryTransform.Find("RecordPeriod").GetComponent<Text>().text = (recordEntry.period > 0.0f) ? recordEntry.period.ToString("f2") : "-";
        entryTransform.Find("RecordPeriodSquare").GetComponent<Text>().text = (recordEntry.periodSquare > 0.0f) ? recordEntry.periodSquare.ToString("f2") : "-";

        transformList.Add(entryTransform);
    }

    public void deleteTemplate()
    {
        foreach (Transform transform in entryContainer)
        {
            if (transform.name == "RecordTemplate(Clone)")
            {
                Destroy(transform.gameObject);
            }
        }
    }
}
