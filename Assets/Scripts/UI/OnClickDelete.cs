using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnClickDelete : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData ped)
    {
        Destroy(gameObject);
    }
}
