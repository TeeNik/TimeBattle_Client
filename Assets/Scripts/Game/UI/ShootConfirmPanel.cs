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
            ++_value;
            Text.text = _value.ToString();
            CheckButtons();
        }
    }

    private void DownClick()
    {
        if (_value > _min)
        {
            ++_value;
            Text.text = _value.ToString();
            CheckButtons();
        }
    }

    private void CheckButtons()
    {
        Up.interactable = _value == _max;
        Down.interactable = _value == _min;

    }

    public int GetValue()
    {
        return _value;
    }
}
