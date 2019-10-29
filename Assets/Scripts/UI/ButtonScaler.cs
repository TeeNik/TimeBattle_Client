using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScaler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Vector3 TargetValue;
    public float TargetTime;

    public void OnPointerDown(PointerEventData eventData)
    {
        gameObject.transform.DOScale(TargetValue, TargetTime);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        gameObject.transform.DOScale(Vector3.one, TargetTime);
    }
}
