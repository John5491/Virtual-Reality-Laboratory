using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RecordReplacement : MonoBehaviour
{
    [SerializeField] Text description;
    [SerializeField] Text record1;
    [SerializeField] Text record2;
    [SerializeField] Text record3;
    [SerializeField] GameObject saveBtn;
    [SerializeField] GameObject slider;

    private void OnEnable()
    {
        float curLength = slider.GetComponent<LengthChanger>().curLength;
        List<Records> table = saveBtn.GetComponent<Table>().table;
        Records temp = table.Find(x => x.length == curLength);

        description.text = "Select the field to replace for length: " + curLength.ToString("f1") + "M";
        record1.text = TimeSpan.FromSeconds(temp.timeRecord[0]).ToString(@"mm\:ss\:ff");
        record2.text = TimeSpan.FromSeconds(temp.timeRecord[1]).ToString(@"mm\:ss\:ff");
        record3.text = TimeSpan.FromSeconds(temp.timeRecord[2]).ToString(@"mm\:ss\:ff");
    }
}
