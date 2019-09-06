using UnityEngine;

public class CharacterPrediction : MonoBehaviour
{
    private Character _reference;

    public void Init(Character ch)
    {
        _reference = ch;
    }

    //TODO remove this
    void OnDestroy()
    {
        _reference.transform.position = transform.position;
    }
}
