public class Character : Entity
{

    public Weapon Weapon = new Pistol();

    private void OnMouseDown()
    {
        Game.I.InputController.SelectCharacter(this);
    }

    private void SetVisibility(bool isVisible)
    {
        gameObject.SetActive(isVisible);
    }
}
