using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] GameObject levelButtonPrefab;
    [SerializeField] GameObject levelButtonHolder;
    [SerializeField] List<string> title;
    [SerializeField] List<string> description;

    int count;
    [SerializeField] List<Texture2D> textures;

    private void Awake()
    {
        count = SceneManager.sceneCountInBuildSettings - 1;
    }

    public void OnEnable()
    {
        if (levelButtonHolder != null && levelButtonPrefab != null)
        {
            //GetAllThumbnailInFolder();
            GetAllThumbnailInFolderUsingR();
            for (int i = 0; i < Mathf.Ceil(count / 4f); i++)
            {
                for (int j = 0; j < (i == Mathf.Ceil(count / 4f) - 1 && count % 4 != 0 ? count % 4 : 4); j++)
                {
                    int scene = i + j + i * 3;
                    GameObject button = Instantiate(levelButtonPrefab, levelButtonHolder.transform);
                    button.GetComponent<Button>().onClick.AddListener(delegate
                    {
                        loadScene(scene + 1);
                    });
                    Vector2 position = new Vector2((j * 425 - 650), (i < 1 ? 250f : -150));
                    button.transform.localPosition = new Vector2(0, 0);
                    foreach (Transform child in button.transform)
                    {
                        if (child.name == "Title")
                        {
                            child.GetComponent<Text>().text = title[scene];
                        }
                        if (child.name == "Description")
                        {
                            child.GetComponent<Text>().text = description[scene];
                        }
                        if (child.name == "Image")
                        {
                            child.GetComponent<RawImage>().texture = textures[scene];
                        }
                    }
                }
            }
        }
    }

    private void GetAllThumbnailInFolder()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo($"{Application.dataPath}/Resources/Thumbnail/");
        List<FileInfo> files = new List<FileInfo>(directoryInfo.GetFiles("*.png"));
        textures = new List<Texture2D>();
        foreach (FileInfo file in files)
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
            textures.Add(tempTexture);
        }
    }

    public void  OnDisable()
    {
        if (levelButtonHolder != null && levelButtonPrefab != null)
        {
            foreach (Transform transform in levelButtonHolder.transform)
            {
                Destroy(transform.gameObject);
            }
        }
    }

    private void GetAllThumbnailInFolderUsingR()
    {
        object[] os = Resources.LoadAll("Thumbnail", typeof(Texture2D));
        foreach(object o in os)
        {
            textures.Add((Texture2D) o);
        }
    }

    public void loadScene(int sceneNo)
    {
        SceneManager.LoadScene(sceneNo);
    }

    public void endGame()
    {
        Application.Quit();
    }
}
