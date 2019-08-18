using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActionController : MonoBehaviour
{

    [SerializeField]
    private GameObject ActionPanel;

    [SerializeField]
    private Transform SelectionTarget;

    private ActionType _selectedAction;

    public void ShowActionPanel(Character ch)
    {
        ActionPanel.SetActive(true);
        SelectionTarget.position = ch.transform.position;
    }


    public void HideActionPanel()
    {
        ActionPanel.SetActive(false);
    }

    private void Update()
    {
        if (_selectedAction != ActionType.None)
        {
            if (Input.GetMouseButton(0))
            {

            }
            else
            {

            }
        }
    }

    private void OnActionClick()
    {

    }

}
