public class Character : Entity
{

    private void OnMouseDown()
    {
        Game.I.InputController.SelectCharacter(this);
    }
}
