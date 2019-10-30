using TMPro;
using UnityEngine;

public class CharacterView : MonoBehaviour
{

    [SerializeField] private Transform _container; 
    private Animator _animator;

    public bool IsStanding { get; private set; }
    public bool IsWalking { get; private set; }

    public void SetInfo(OperativeInfoComponent info)
    {
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

    public void PlayWalkAnimation(bool isWalking)
    {
        IsWalking = isWalking;
        _animator.SetBool("walk", isWalking);
    }

    public void PlayStandAnimation(bool isStanding)
    {
        IsStanding = isStanding;
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
