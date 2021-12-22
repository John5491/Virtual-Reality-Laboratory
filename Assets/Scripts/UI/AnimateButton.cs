using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnimateButton : MonoBehaviour
{
    [SerializeField] float animationTime = 1.0f;
    [SerializeField] float animationInitalDelay = 0.0f;
    [SerializeField] float animationLoopDelay = 1.0f;
    [SerializeField] float posYOffset = 0.5f;
    GameObject Midlayer;
    private bool playAnimation = false;

    private void OnEnable()
    {
        Midlayer = transform.Find("OuterLayer/MidLayer").gameObject;
        playAnimation = false;
        StartCoroutine(AnimationInitalDelay());
        Midlayer.GetComponent<HorizontalLayoutGroup>().padding = new RectOffset(0, 0, 0, 15);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        playAnimation = false;
        Midlayer.GetComponent<HorizontalLayoutGroup>().padding = new RectOffset(0, 0, 0, 15);
    }

    // Update is called once per frame
    void Update()
    {
        if(playAnimation)
        {
            playAnimation = false;
            StartCoroutine(ResetAnimationLoop());
            StartCoroutine(PlayAnimation());
        }
    }

    private IEnumerator AnimationInitalDelay()
    {
        yield return new WaitForSeconds(animationInitalDelay);
        playAnimation = true;
    }

    private IEnumerator ResetAnimationLoop()
    {
        yield return new WaitForSeconds(animationLoopDelay);
        playAnimation = true;
    }

    private IEnumerator PlayAnimation()
    {
        Midlayer.GetComponent<HorizontalLayoutGroup>().padding = new RectOffset(0, 0, 0, 5);
        transform.position = new Vector3(transform.position.x, transform.position.y + posYOffset, transform.position.z);
        yield return new WaitForSeconds(animationTime);
        Midlayer.GetComponent<HorizontalLayoutGroup>().padding = new RectOffset(0, 0, 0, 15);
        transform.position = new Vector3(transform.position.x, transform.position.y - posYOffset, transform.position.z);
    }
}
