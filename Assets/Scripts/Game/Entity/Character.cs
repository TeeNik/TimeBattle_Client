public class Character : Entity
{

    private void OnMouseDown()
    {
        RoomModel.I.InputController.SelectCharacter(this);
    }
}
