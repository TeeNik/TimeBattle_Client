public class Character : Entity
{

    private void OnMouseDown()
    {
        Game.I.UserInputController.SelectCharacter(this);
    }

    private void SetVisibility(bool isVisible)
    {
        gameObject.SetActive(isVisible);
    }
}
