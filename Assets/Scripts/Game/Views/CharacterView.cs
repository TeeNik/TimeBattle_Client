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
        var name = $"{info.OperativeType}_{info.Owner}";
        //var sprite = $"Assault_{info.Owner}";
        //_spriteRenderer.sprite = ResourceManager.Instance.GetSprite(sprite);
        var model = ResourceManager.Instance.GetCharacterModel(name);
        Instantiate(model, _container);
    }

    public void Rotate(Point from, Point to)
    {
        Rotation direction;
        if (from.X != to.X)
        {
            direction = from.X > to.X ? Rotation.Up : Rotation.Down;
        }
        else
        {
            direction = from.Y > to.Y ? Rotation.Left : Rotation.Right;
        }
        _container.localEulerAngles = new Vector3(0, 0, (int)direction);
    }

    private enum Rotation
    {
        Right = 0,
        Up = 90,
        Left = 180,
        Down = 270
    }

}
