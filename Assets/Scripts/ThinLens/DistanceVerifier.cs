using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DistanceVerifier : MonoBehaviour
{
    [SerializeField] GameObject objectCanvas;
    [SerializeField] GameObject imageCanvas;
    [SerializeField] GameObject lensCanvas;
    [SerializeField] GameObject virtualImageToShow;
    [SerializeField] GameObject realImageToShow_CAM;
    [SerializeField] Material realImageMaterial;
    [SerializeField] RenderTexture magnifyingRenderTexture;

    [SerializeField] float vectorToCM = 4.0f;
    [SerializeField] float focalLength = 15.0f;
    [SerializeField] float plusMinusFloatingPoint = 0.15f;

    private float initialScale;
    private float magnification;
    private float distanceForImageToShow;
    private float objectToLensInCM;
    private float lensToImageInCM;

    private void Awake()
    {
        initialScale = virtualImageToShow.transform.localScale.x;
    }

    private void Update()
    {
        GetDistance();
        ShowImage();
    }
    
    public void GetDistance()
    {
        float objectToLens = objectCanvas.transform.position.x - lensCanvas.transform.position.x;
        objectToLensInCM = Math.Abs(objectToLens * vectorToCM);
        float lensToImage = lensCanvas.transform.position.x - imageCanvas.transform.position.x;
        lensToImageInCM = Math.Abs(lensToImage * vectorToCM);
        distanceForImageToShow = (float) Math.Pow((1 / focalLength) - (1 / objectToLensInCM), -1.0);
        magnification = (-distanceForImageToShow / objectToLensInCM);
    }

    public void DebugDistance()
    {
        Debug.Log("Object to Len: " + objectToLensInCM.ToString() + "\n"
                + "Lens To Image: " + lensToImageInCM.ToString() + "\n"
                + "Distance needed: " + distanceForImageToShow.ToString() + "\n"
                + "Magnification: " + magnification.ToString());
    }

    void ShowImage()
    {
        virtualImageToShow.SetActive(false);
        if (magnification < 0)
        {
            if (lensToImageInCM < distanceForImageToShow + plusMinusFloatingPoint && lensToImageInCM > distanceForImageToShow - plusMinusFloatingPoint)
            {
                virtualImageToShow.SetActive(true);
                virtualImageToShow.GetComponent<ImageRenderer>().UpdateTexture(magnification);

                Material imageMat = virtualImageToShow.GetComponent<Renderer>().material;
                float difference = Math.Abs(distanceForImageToShow - lensToImageInCM);
                float blurAmount = (float) Math.Pow(difference, 2) + difference;
                blurAmount = Mathf.Clamp(blurAmount, 0f, 50f);
                imageMat.SetFloat("_Blur", blurAmount);

                /*Debug.Log("Object to Lens (cm): " + objectToLensInCM);
                Debug.Log("Lens To Image (cm): " + lensToImageInCM);
                Debug.Log("Magnification: " + magnification);*/

            }
            realImageMaterial.mainTexture = null;
            Color color = realImageMaterial.color;
            color.a = .1f;
            realImageMaterial.color = color;
        }
        else
        {
            realImageMaterial.mainTexture = magnifyingRenderTexture;
            Color color = realImageMaterial.color;
            color.a = 1.0f;
            realImageMaterial.color = color;
            realImageToShow_CAM.GetComponent<Camera>().fieldOfView = Mathf.Clamp(60.0f * (1/Mathf.Abs(magnification)), 0f, 60f);
        }
        /*else
        {
            realImageToShow.SetActive(true);
            realImageToShow.GetComponent<ImageRenderer>().UpdateTexture(magnification);

            Material imageMat = realImageToShow.GetComponent<Renderer>().material;
            float difference = Math.Abs(distanceForImageToShow - lensToImageInCM);
            float blurAmount = (float)Math.Pow(difference, 2) + difference;
            blurAmount = Mathf.Clamp(blurAmount, 0f, 50f);
            imageMat.SetFloat("_Blur", blurAmount);
        }*/
    }
}
