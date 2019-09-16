public class Character : Entity
{

    private void OnMouseDown()
    {
        Game.I.InputController.SelectCharacter(this);
    }

    private void SetVisibility(bool isVisible)
    {
        gameObject.SetActive(isVisible);
    }
}
