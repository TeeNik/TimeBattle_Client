using System.Collections.Generic;
using System.Linq;

public class ShootInput : ActionInput
{
    private readonly PredictionMap _prediction;
    private List<Point> _range;
    private CharacterActionController _ac;

    private const int MinDuration = 3;

    private Character _char;
    private Point _position;
    private Weapon _weapon;

    public ActionType GetActionType()
    {
        return ActionType.Shoot;
    }

    public ShootInput(PredictionMap prediction, CharacterActionController ac)
    {
        _prediction = prediction;
        _ac = ac;
    }

    public void ProduceInput()
    {
        var duration = _ac.ShootConfirmPanel.GetValue();
        var comp = new ShootComponent(_range, 3);
        Game.I.UserInputController.ProduceInput(GetActionType(), comp);
        _range = null;
        _ac.ShootConfirmPanel.Hide();
        _ac._isWaitForConfirm = false;
    }

    public void WaitForConfirm()
    {
        if (_range != null)
        {
            _prediction.DrawShootInput(_range);
            var charInfo = _char.GetEcsComponent<CharacterActionComponent>();
            _ac.ShootConfirmPanel.Show(MinDuration, charInfo.Energy);
            _ac._isWaitForConfirm = true;
        }

        Game.I.MapController.OutlinePool.ReturnAll();
    }

    public void Start(Character ch)
    {
        _char = ch;
        _weapon = ch.GetEcsComponent<ShootComponent>().Weapon;
        var tile = Game.I.MapController.GetTileByVector3(ch.transform.position);
        _position = new Point(tile.x, tile.y);
        DrawRanges();
    }

    private List<Point> fullRange;

    private void DrawRanges()
    {
        var pool = Game.I.MapController.OutlinePool;
        fullRange = new List<Point>();
        var map = Game.I.MapController;
        var mapData = map.MapDatas;

        foreach (var range in _weapon.GetAvailableRange(_position))
        {
            fullRange.AddRange(range);
        }

        foreach (var point in fullRange)
        {
            var obj = pool.GetFromPool();
            obj.transform.position = Game.I.MapController.GetTileWorldPosition(point);
        }
    }

    public void Update()
    {
        var map = Game.I.MapController;
        var tile = map.GetTileByMouse();
        var mousePoint = new Point(tile.x, tile.y);

        foreach (var range in _weapon.GetAvailableRange(_position))
        {
            if (range.Any(r => r.Equals(mousePoint)))
            {
                _range = range;
                _prediction.DrawShootInput(range);
                break;
            }
            _range = null;
            _prediction.ClearLayer(Layers.Temporary);
        }
    }
}
