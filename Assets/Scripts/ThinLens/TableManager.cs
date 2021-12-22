using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableManager : MonoBehaviour
{
    [SerializeField] Transform entryContainer;
    [SerializeField] Transform entryTemplate;
    [SerializeField] float templateHeight = 50f;
    [SerializeField] GameObject controlPanel;


    List<ThinLensRecord> table;
    private List<Transform> recordEntryTransfromList;

    private void OnEnable()
    {
        entryTemplate.gameObject.SetActive(false);
        table = controlPanel.GetComponent<ThinLensSaveRecord>().GetRecordList();

        sortTable();
        recordEntryTransfromList = new List<Transform>();

        foreach (ThinLensRecord item in table)
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
                if (table[j].getObjectDistance() < table[i].getObjectDistance())
                {
                    ThinLensRecord tmp = table[i];
                    table[i] = table[j];
                    table[j] = tmp;
                }
            }
        }
    }

    private void CreateRecordInTable(ThinLensRecord recordEntry, Transform container, List<Transform> transformList)
    {
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        entryTransform.Find("RecordColumn1").GetComponent<Text>().text = recordEntry.getObjectDistance().ToString();
        entryTransform.Find("RecordColumn2").GetComponent<Text>().text = recordEntry.getImageDistance().ToString(); ;
        entryTransform.Find("RecordColumn3").GetComponent<Text>().text = recordEntry.getInverseObjectDistance().ToString();
        entryTransform.Find("RecordColumn4").GetComponent<Text>().text = recordEntry.getInverseImageDistance().ToString();
        entryTransform.Find("RecordColumn5").GetComponent<Text>().text = recordEntry.getFocalLength().ToString();

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
