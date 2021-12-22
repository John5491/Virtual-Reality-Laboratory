using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RulerInitialiser : MonoBehaviour
{
    [SerializeField] GameObject measure1;
    [SerializeField] float gap = 0.23f;
    [SerializeField] int count = 100;

    // Start is called before the first frame update
    void Start()
    {
        float offset = 0.01f;
        for(int i = 0; i < count; i++)
        {
            if(i%10 == 0 && i >= 10)
            {
                offset += 0.005f;
            }
            GameObject measure = Instantiate(measure1, measure1.transform.parent);
            RectTransform measureRectTransform = measure.GetComponent<RectTransform>();
            Vector3 pos = measure1.GetComponent<RectTransform>().localPosition;
            pos = new Vector3(pos.x + offset + (gap * (i + 1)), pos.y, pos.z);
            measureRectTransform.localPosition = pos;
            TextMeshProUGUI text = measure.GetComponent<TextMeshProUGUI>();
            text.text = (i + 1).ToString();
        }
    }
}
