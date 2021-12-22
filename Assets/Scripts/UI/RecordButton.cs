using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RecordButton : MonoBehaviour
{
    [SerializeField] GameObject saveBtn;
    [SerializeField] GameObject lengthPanel;
    [SerializeField] GameObject detailsPanelHolder;
    [SerializeField] GameObject detailsPanel;
    [SerializeField] GameObject entryContainer;
    [SerializeField] GameObject entryTemplate;
    [SerializeField] float templateHeight = 100f;
    [Space]
    [SerializeField] Text errorMsg;
    [SerializeField] InputField inputField;

    public bool faded = false;

    List<Records> table;
    int freq;

    private void OnEnable()
    {
        entryTemplate.gameObject.SetActive(false);
        table = saveBtn.GetComponent<Table>().table;

        sortTable();
        int curPos = 0;
        foreach (Records item in table)
        {
            CreateRecordBtn(item, entryContainer, curPos);
            curPos++;
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

    private void CreateRecordBtn(Records recordEntry, GameObject container, int curPos)
    {
        GameObject entryTransform = Instantiate(entryTemplate, container.transform, false);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, 750f - (templateHeight * curPos));
        entryTransform.gameObject.SetActive(true);
        
        entryTransform.transform.Find("Text").GetComponent<Text>().text = recordEntry.length.ToString("f1");
        entryTransform.GetComponent<RecordDetails>().length = recordEntry.length;
    }

    public void deleteTemplate()
    {
        foreach (Transform transform in entryContainer.transform)
        {
            if (transform.name == "Length(Clone)")
            {
                Destroy(transform.gameObject);
            }
        }
    }

    public void resetPanelPos()
    {
        lengthPanel.GetComponent<Animator>().SetTrigger("exit");
        detailsPanelHolder.GetComponent<Animator>().SetTrigger("exit");
        detailsPanel.GetComponent<CanvasGroup>().alpha = 0.0f;
        faded = false;
    }

    public void calculatePeriod()
    {
        var isNumeric = int.TryParse(inputField.text, out freq);
        if (!isNumeric)
        {
            errorMsg.gameObject.SetActive(true);
            return;
        }
        else
        {
            errorMsg.gameObject.SetActive(false);
            foreach (Records item in table)
            {
                item.updateFrequency(freq);
            }
        }
    }
}
