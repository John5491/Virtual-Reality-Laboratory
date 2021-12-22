using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThinLensSaveRecord : MonoBehaviour
{
    [SerializeField] InputField objectDistanceInputField;
    [SerializeField] InputField imageDistanceInputField;
    [SerializeField] Button saveButton;

    public static List<ThinLensRecord> recordList;

    private void Start()
    {
        recordList = new List<ThinLensRecord>();
    }

    public List<ThinLensRecord> GetRecordList()
    {
        return recordList;
    }

    public void SaveRecord()
    {
        float objectDistance = float.Parse(objectDistanceInputField.text);
        float imageDistance = float.Parse(imageDistanceInputField.text);
        objectDistanceInputField.text = "";
        imageDistanceInputField.text = "";
        recordList.Add(new ThinLensRecord(objectDistance, imageDistance));
    }

    public void CheckInputField()
    {
        if(float.TryParse(objectDistanceInputField.text, out _) && float.TryParse(imageDistanceInputField.text, out _))
        {
            saveButton.interactable = true;
        }
        else
        {
            saveButton.interactable = false;
        }
    }
}
