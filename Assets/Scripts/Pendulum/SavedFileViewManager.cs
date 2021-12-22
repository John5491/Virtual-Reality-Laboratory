using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SavedFileViewManager : MonoBehaviour
{
    [SerializeField] ScreenShot screenshot;
    [SerializeField] GameObject buttonGroup;
    [SerializeField] GameObject imageEnlarger;
    [SerializeField] Transform groupHolder;

    List<Texture2D> textures;
    List<FileInfo> files;

    public void SetupSavedFileView()
    {
        foreach (Transform child in groupHolder)
        {
            Destroy(child.gameObject);
        }

        screenshot.GetAllScreenShotInFolder();
        textures = screenshot.images;
        files = screenshot.files;
        
        for (int i = 0; i < Mathf.Ceil(textures.Count / 3f); i++)
        {
            for (int j = 0; j < (i == Mathf.Ceil(textures.Count / 3f) - 1 && textures.Count % 3 != 0 ? textures.Count % 3 : 3); j++) {
                GameObject group = Instantiate(buttonGroup, groupHolder);
                group.GetComponent<Button>().onClick.AddListener(delegate {
                    imageEnlarger.transform.parent.gameObject.SetActive(true);
                    GetImagePressed(group);
                });
                Vector2 position = new Vector2((j * 220 - 220), -(i % 3 * 250 - 250));
                group.transform.localPosition = position;
                foreach (Transform child in group.transform)
                {
                    if(child.name == "Text")
                    {
                        child.GetComponent<Text>().text = files[i + j + i * 2].Name;
                    }
                    if(child.name == "Image")
                    {
                        child.GetComponent<RawImage>().texture = textures[i + j + i * 2];
                    }
                }
            }
        }
    }

    public void GetImagePressed(GameObject thisObject)
    {
        foreach(Transform child in thisObject.transform)
        {
            if (child.name == "Image")
            {
                imageEnlarger.GetComponent<RawImage>().texture = child.GetComponent<RawImage>().texture;
            }
        }
    }
}
