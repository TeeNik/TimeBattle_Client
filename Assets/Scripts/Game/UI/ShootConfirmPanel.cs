using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShootConfirmPanel : MonoBehaviour
{

    public TMP_Text Text;
    public Button Up;
    public Button Down;

    private int _min;
    private int _max;
    private int _value;

    private void Start()
    {
        Up.onClick.AddListener(UpClick);
        Down.onClick.AddListener(DownClick);
    }

    public void Show(int min, int max)
    {
        _min = min;
        _max = max;
        SetValue(min);
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void UpClick()
    {
        if(_value < _max)
        {
            SetValue(_value + 1);
        }
    }

    private void DownClick()
    {
        if (_value > _min)
        {
            SetValue(_value - 1);
        }
    }

    private void CheckButtons()
    {
        Up.interactable = _value != _max;
        Down.interactable = _value != _min;

    }

    private void SetValue(int value)
    {
        _value = value;
        Text.text = _value.ToString();
        CheckButtons();
    }

    public int GetValue()
    {
        return _value;
    }
}
