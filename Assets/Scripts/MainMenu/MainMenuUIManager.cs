using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] GameObject LevelMenu;
    [SerializeField] GameObject MainMenu;
    [SerializeField] float delayEnable = 0.5f;

    public void EnableLevelMenu()
    {
        StartCoroutine(DelayBeforeEnableLvlMenu());
    }

    private IEnumerator DelayBeforeEnableLvlMenu()
    {
        yield return new WaitForSeconds(delayEnable);
        LevelMenu.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void EnableMainMenu()
    {
        StartCoroutine(DelayBeforeEnableMainMenu());
    }

    private IEnumerator DelayBeforeEnableMainMenu()
    {
        yield return new WaitForSeconds(delayEnable);
        LevelMenu.SetActive(false);
        MainMenu.SetActive(true);
    }
}
