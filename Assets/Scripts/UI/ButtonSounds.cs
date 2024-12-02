using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSounds : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Instance.PlayUISFX("CLICK", 1f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.PlayUISFX("HOVERING", 1f);
    }
}
