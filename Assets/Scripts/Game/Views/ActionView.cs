using TMPro;
using UnityEngine;

public class ActionView : MonoBehaviour
{

    [SerializeField] private GameObject View;
    [SerializeField] private TMP_Text Text;

    public void SetValue(int value)
    {
        View.SetActive(value > 0);
        Text.text = value.ToString();
    }

}
