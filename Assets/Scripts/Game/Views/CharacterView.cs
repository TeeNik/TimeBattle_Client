using TMPro;
using UnityEngine;

public class CharacterView : MonoBehaviour
{

    [SerializeField] private TMP_Text _title;
    [SerializeField] private SpriteRenderer _spriteRenderer; 
    [SerializeField] private Transform _container; 

    public void SetInfo(OperativeInfoComponent info)
    {
        _title.text = info.OperativeType.ToString();
        //var sprite = $"{info.OperativeType}_{info.Owner}";
        var sprite = $"Assault_{info.Owner}";
        _spriteRenderer.sprite = ResourceManager.Instance.GetSprite(sprite);
        var model = ResourceManager.Instance.Get
    }

}
