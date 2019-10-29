using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Vector3 _origin;
    private Vector3 _diference;
    private bool _drag;

    public readonly Vector2 VecticalClamp = new Vector2(-9.7f, -5.3f);
    public readonly float MaxDif = 1.5f;

    void LateUpdate()
    {
        var position = Camera.main.transform.position;
        if (Input.GetMouseButton(2))
        {
            _diference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - position;

            Debug.Log(_diference);
            if (_drag == false)
            {
                _drag = true;
                _origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            _drag = false;
        }
        if (_drag)
        {
            var pos = _origin - _diference;
            var y = Mathf.Clamp(pos.y, VecticalClamp.x, VecticalClamp.y);
            Camera.main.transform.position = new Vector3(position.x, y, pos.z);
        }

        if (Input.GetMouseButton(1))
        {
            DebugPoint();
        }
    }

    void DebugPoint()
    {
        var point = Game.I.MapController.GetTileByMouse();
        Debug.Log(point);
    }
}