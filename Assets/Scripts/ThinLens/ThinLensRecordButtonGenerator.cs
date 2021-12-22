using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThinLensRecordButtonGenerator : MonoBehaviour
{
    [SerializeField] GameObject controlPanel;
    [SerializeField] GameObject recordButtonPanel;
    [SerializeField] GameObject detailsPanelHolder;
    [SerializeField] GameObject detailsPanel;
    [SerializeField] GameObject entryContainer;
    [SerializeField] GameObject entryTemplate;
    [SerializeField] float templateHeight = 100f;

    public bool faded = false;

    List<ThinLensRecord> records;

    private void OnEnable()
    {
        entryTemplate.gameObject.SetActive(false);
        records = controlPanel.GetComponent<ThinLensSaveRecord>().GetRecordList();

        sortRecords();
        int curPos = 0;
        foreach (ThinLensRecord item in records)
        {
            CreateRecordBtn(item, entryContainer, curPos);
            curPos++;
        }
    }

    private void sortRecords()
    {
        for (int i = 0; i < records.Count; i++)
        {
            for (int j = i + 1; j < records.Count; j++)
            {
                if (records[j].getObjectDistance() < records[i].getObjectDistance())
                {
                    ThinLensRecord tmp = records[i];
                    records[i] = records[j];
                    records[j] = tmp;
                }
            }
        }
    }

    private void CreateRecordBtn(ThinLensRecord recordEntry, GameObject container, int curPos)
    {
        GameObject entryTransform = Instantiate(entryTemplate, container.transform, false);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, 750f - (templateHeight * curPos));
        entryTransform.gameObject.SetActive(true);

        entryTransform.transform.Find("Text").GetComponent<Text>().text = recordEntry.getObjectDistance().ToString("f1");
        entryTransform.GetComponent<ThinLensRecordDetailsManager>().objectDistance = recordEntry.getObjectDistance();
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
        recordButtonPanel.GetComponent<Animator>().SetTrigger("exit");
        detailsPanelHolder.GetComponent<Animator>().SetTrigger("exit");
        detailsPanel.GetComponent<CanvasGroup>().alpha = 0.0f;
        faded = false;
    }
}
