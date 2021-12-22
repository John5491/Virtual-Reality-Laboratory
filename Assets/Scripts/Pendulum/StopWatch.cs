using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StopWatch : MonoBehaviour
{
    bool stopwatchActive = false;
    public float currentTime;
    public Text currentTimeText;
    public GameObject pendulum;
    public Button stopButton;


    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(stopwatchActive)
        {
            currentTime = currentTime + Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = "Time : " + time.ToString(@"mm\:ss\:ff");
    }

    public void StartStopwatch()
    {
        stopwatchActive = true;
        stopButton.interactable = true;
    }

    public void StopStopwatch()
    {
        stopwatchActive = false;
        stopButton.interactable = false;
        pendulum.GetComponent<Rigidbody>().isKinematic = true;
    }
}
