public class Character : Entity
{

    public Weapon Weapon = new Pistol();

    private void OnMouseDown()
    {
        Game.I.InputController.SelectCharacter(this);
    }
}
