using TMPro;
using UnityEngine;

public class CharacterView : MonoBehaviour
{

    [SerializeField] private TMP_Text _title;
    [SerializeField] private SpriteRenderer _spriteRenderer; 
    [SerializeField] private Transform _container; 
    [SerializeField] private Animator _animator;

    public void SetInfo(OperativeInfoComponent info)
    {
        _title.text = info.OperativeType.ToString();
        var name = $"{info.OperativeType}_{info.Owner}";
        //var sprite = $"Assault_{info.Owner}";
        //_spriteRenderer.sprite = ResourceManager.Instance.GetSprite(sprite);
        var model = ResourceManager.Instance.GetCharacterModel(name);
        Instantiate(model, _container);
        _animator = GetComponentInChildren<Animator>();
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

    public void PlayMoveAnimation(bool isMoving)
    {
        _animator.SetBool("walk", isMoving);
    }

    public void PlayStandAnimation(bool isStanding)
    {
        _animator.SetBool("stand", isStanding);
    }

    public void PlayShootAnimation()
    {
        _animator.SetTrigger("shoot");
    }

    private enum Rotation
    {
        Right = 0,
        Up = 90,
        Left = 180,
        Down = 270
    }

}
