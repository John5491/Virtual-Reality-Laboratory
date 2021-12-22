using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RecordsHandler : MonoBehaviour
{
    [SerializeField] List<string> colNames;
    [SerializeField] List<string> colUnits;
    [SerializeField] List<int> columnIndicator;
    private List<Records2> records;

    public List<Records2> Records
    {
        get { return records; }
    }

    public void AddToRecords(Records2 record)
    {
        records.Add(record);
    }

    public void ChangeRecord(int index, Records2 record)
    {
        records[index] = record;
    }

    public int GetColumnIndex(string colName)
    {
        return colNames.IndexOf(colName);
    }
    
}
