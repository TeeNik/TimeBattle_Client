using UnityEngine;

public class CharacterPrediction : MonoBehaviour
{
    private Character _reference;

    public void Init(Character ch)
    {
        _reference = ch;
    }

    public void MoveReferance()
    {
        _reference.transform.position = transform.position;
    }
}
