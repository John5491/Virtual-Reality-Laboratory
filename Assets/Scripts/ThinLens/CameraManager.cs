using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject distanceCamera;
    [SerializeField] GameObject camera3;
    [SerializeField] GameObject camera4;

    [SerializeField] Rect rect1 = new Rect(0.7f, 0.7f, 0.25f, 0.25f);
    [SerializeField] Rect rect2 = new Rect(0.7f, 0.7f, 0.25f, 0.25f);

    private int keyPressed = -1;

    private void Update()
    {
        if(Input.GetKeyDown("1"))
            keyPressed = 1;
        else if (Input.GetKeyDown("2"))
            keyPressed = 2;
        else if (Input.GetKeyDown("3"))
            keyPressed = 3;
        else if (Input.GetKeyDown("4"))
            keyPressed = 4;

        if(keyPressed > 0)
        {
            ResetCamera3(false);
            mainCamera.SetActive(false);
            distanceCamera.SetActive(false);
            camera3.SetActive(false);
            camera4.SetActive(false);

            switch (keyPressed)
            {
                case 1:
                    mainCamera.SetActive(true);
                    break;

                case 2:
                    distanceCamera.SetActive(true);
                    ResetCamera3(true);
                    camera3.SetActive(true);
                    camera4.SetActive(true);
                    break;

                case 3:
                    camera3.SetActive(true);
                    break;

                case 4:
                    camera4.SetActive(true);
                    break;

            }
            keyPressed = -1;
        }
    }

    void ResetCamera3(bool isMini)
    {
        if(isMini)
        {
            camera3.GetComponent<Camera>().rect = rect1;
            camera4.GetComponent<Camera>().rect = rect2;
        }
        else
        {
            camera3.GetComponent<Camera>().rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
            camera4.GetComponent<Camera>().rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
        }
    }


}
