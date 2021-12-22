using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageRenderer : MonoBehaviour
{
    [SerializeField] Texture texture;
    [SerializeField] RenderTexture renderTexture;
    [SerializeField] Material scaleShaderMaterial;

    public float scale = 1.0f;

    private float oldScale;

    public bool useUpdate = false;

    public void UpdateTexture(float magnification)
    {
        if (!useUpdate)
        {
            scaleShaderMaterial.SetFloat("_Scale", (1.0f / magnification));
            renderTexture.Release();
            Graphics.Blit(texture, renderTexture, scaleShaderMaterial);
        }
    }


    /*void Update()
    {
        if (useUpdate)
        {
            if (oldScale != scale)
            {
                oldScale = scale;
                scaleShaderMaterial.SetFloat("_Scale", scale);
                renderTexture.Release();
                Graphics.Blit(texture, renderTexture, scaleShaderMaterial);
            }
        }
    }


    void Update()
    {
        float difference = Mathf.Abs(objectCanvas.transform.position.x - magnifyingCamera.transform.position.x);
        float newNearClip = (float) (Mathf.Pow(difference, 2) * curveSensitivity) + 0.01f;
        newNearClip = newNearClip * 0.018027223f;
        float clampedNearClip = Mathf.Clamp(newNearClip, 0.01f, 2.35f);
        magnifyingCamera.GetComponent<Camera>().nearClipPlane = clampedNearClip;
    }*/
}
