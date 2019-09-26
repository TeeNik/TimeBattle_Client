using TMPro;
using UnityEngine;

public class InfoView : MonoBehaviour
{

    [SerializeField] private TMP_Text _title;
    [SerializeField] private SpriteRenderer _spriteRenderer; 

    public void SetInfo(OperativeInfoComponent info)
    {
        _title.text = info.OperativeType.ToString();
        //var sprite = $"{info.OperativeType}_{info.Owner}";
        var sprite = $"Assault_{info.Owner}";
        _spriteRenderer.sprite = ResourceManager.Instance.GetSprite(sprite);
    }

}
