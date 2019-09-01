using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Image HealthBar;

    public void SetHealth(float health)
    {
        HealthBar.fillAmount = health;
    }
}
