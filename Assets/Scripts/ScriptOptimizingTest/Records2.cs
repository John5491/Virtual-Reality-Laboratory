using System;
using System.Collections.Generic;

public class Records2
{
    private List<object> recordColumn;
    public List<object> RecordColumn
    {
        get { return recordColumn; }
        set { recordColumn = value; }
    }

    public Records2(List<object> recordColumn)
    {
        this.recordColumn = recordColumn;
    }

    public object GetElement(int index)
    {
        return recordColumn[index];
    }

    public void SetElement(int index, object value)
    {
        recordColumn[index] = value;
    }

    public void UpdateAllColumn(List<object> recordColumn)
    {
        this.recordColumn = recordColumn;
    }
}
