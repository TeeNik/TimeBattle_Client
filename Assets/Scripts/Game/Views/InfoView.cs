using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoView : MonoBehaviour
{

    [SerializeField] private TMP_Text Title;

    public void SetInfo(OperativeInfoComponent info)
    {
        Title.text = info.OperativeType.ToString();
    }

}
