using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScreenShot : MonoBehaviour
{
    [SerializeField] string screenshotName = "sample";
    [SerializeField] string folderName = "Lab1";
    [SerializeField] RenderTexture targetTexture;
    [SerializeField] RectTransform rectT;
    [SerializeField] int offset = 0;
    [SerializeField] public List<Texture2D> images;
    public List<FileInfo> files;

    int width = 1920;
    int height = 1080;

    // Start is called before the first frame update
    void Start()
    {
        width = System.Convert.ToInt32(rectT.rect.width);
        height = System.Convert.ToInt32(rectT.rect.height);
    }

    public void takeScreenshot()
    {
        ScreenCapture.CaptureScreenshotIntoRenderTexture(targetTexture);
        Texture2D textureToSave = ToTexture2D(targetTexture);
        FlipTextureVertically(textureToSave);
        byte[] imageData = textureToSave.EncodeToPNG();

        if (!Directory.Exists($"{Application.dataPath}/Screenshot/" + folderName + "/"))
        {
            Directory.CreateDirectory($"{Application.dataPath}/Screenshot/" + folderName + "/");
        }

        File.WriteAllBytes($"{Application.dataPath}/Screenshot/"+ folderName + $"/{screenshotName + System.DateTime.Now.ToString("yyyy-MMM-dd-hmmttss")}.png", imageData);
    }

    private Texture2D ToTexture2D(RenderTexture Target)
    {
        Vector2 temp = rectT.transform.position;
        var startX = temp.x - width / 2;
        var startY = (temp.y - height / 2) + offset;

        Texture2D Result = new Texture2D(width, height, TextureFormat.RGB24, false);
        RenderTexture.active = Target;

        Result.ReadPixels(new Rect(startX, startY, width, height), 0, 0);
        Result.Apply();

        RenderTexture.active = null;

        return Result;
    }

    public static void FlipTextureVertically(Texture2D original)
    {
        var originalPixels = original.GetPixels();

        Color[] newPixels = new Color[originalPixels.Length];

        int width = original.width;
        int rows = original.height;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                newPixels[x + y * width] = originalPixels[x + (rows - y - 1) * width];
            }
        }

        original.SetPixels(newPixels);
        original.Apply();
    }

    public void GetAllScreenShotInFolder()
    {
        if (!Directory.Exists($"{Application.dataPath}/Screenshot/" + folderName + "/"))
        {
            Directory.CreateDirectory($"{Application.dataPath}/Screenshot/" + folderName + "/");
        }
        DirectoryInfo directoryInfo = new DirectoryInfo($"{Application.dataPath}/Screenshot/" + folderName + "/");
        files = new List<FileInfo>(directoryInfo.GetFiles("*.png"));
        images = new List<Texture2D>();
        foreach(FileInfo file in files)
        {
            MemoryStream dest = new MemoryStream();
            using (Stream source = file.OpenRead())
            {
                byte[] buffer = new byte[2048];
                int bytesRead;
                while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
                {
                    dest.Write(buffer, 0, bytesRead);
                }
            }

            byte[] imageBytes = dest.ToArray();
            Texture2D tempTexture = new Texture2D(2, 2);
            tempTexture.LoadImage(imageBytes);
            images.Add(tempTexture);
        }
    }
}
