using System.Collections.Generic;
using System.Linq;

public class ThrowGrenadeInput : ActionInput
{

    private GrenadeWeapon _grenade = new GrenadeWeapon();
    private Character _char;
    private Point _position;
    private readonly PredictionMap _prediction;
    private Point _target;
    private CharacterActionController _ac;

    public ActionType GetActionType()
    {
        return ActionType.ThrowGrenade;
    }

    public ThrowGrenadeInput(PredictionMap prediction, CharacterActionController ac)
    {
        _prediction = prediction;
        _ac = ac;
    }

    public void Update()
    {
        var tile = Game.I.MapController.GetTileByMouse();
        var mousePoint = new Point(tile.x, tile.y);

        foreach (var range in _grenade.GetAvailableRange(_position))
        {
            if (range.Any(r => r.Equals(mousePoint)))
            {
                _prediction.DrawShootInput(range);
                DrawExplosionRange(mousePoint);
                break;
            }
            _target = null;
            _prediction.ClearLayer(Layers.Temporary);
        }
    }

    public void ProduceInput()
    {
    }

    private void DrawExplosionRange(Point target)
    {

    }

    public void WaitForConfirm()
    {
    }

    public void Start(Character ch)
    {
    }
}
