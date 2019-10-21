using System.Text;
using TMPro;
using UnityEngine;

public class ExceptionHandler : MonoBehaviour
{

    public GameObject ErrorPanel;
    public TMP_Text Text;

    private readonly StringBuilder _sb = new StringBuilder();

    void Start()
    {
        DontDestroyOnLoad(this);
        Application.logMessageReceived += OnLogCallback;
    }

    private void OnLogCallback(string condition, string stackTrace, LogType type)
    {
        if (type == LogType.Exception || type == LogType.Assert)
        {
            var exceptionText = new StringBuilder();
            exceptionText.AppendLine(type.ToString());
            exceptionText.AppendLine(condition);
            exceptionText.AppendLine(stackTrace);
            _sb.Append(exceptionText);
            Text.text = _sb.ToString();
            ErrorPanel.SetActive(true);
            Application.logMessageReceived -= OnLogCallback;
        }
    }
}
