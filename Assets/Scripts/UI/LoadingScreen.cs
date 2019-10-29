using DG.Tweening;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    public GameObject Circle;

    public const float Time = 1.5f;

    public void Show()
    {
        Circle.transform.DOScale(new Vector3(30, 30, 30), Time).SetEase(Ease.Linear);
    }

    public void Hide()
    {
        Circle.transform.DOScale(new Vector3(0, 0, 0), Time).SetEase(Ease.Linear);
    }
}
