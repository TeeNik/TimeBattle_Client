using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Image HealthBar;
    [SerializeField] private SpriteRenderer BaseSprite;

    public virtual void SetHealth(float health)
    {
        HealthBar.fillAmount = health;
    }

    public virtual void PlayDeath()
    {
        Destroy(gameObject);

        /*BaseSprite.DOColor(Color.red, .1f).SetEase(Ease.Linear).SetLoops(10, LoopType.Yoyo).OnComplete(() =>
        {
        });*/
    }
}
