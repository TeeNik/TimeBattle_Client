using Newtonsoft.Json.Linq;
using UnityEngine;

public class LoginEvent : BaseEventClass
{
    public LoginEvent() : base(EventNames.Login)
    {
    }

    public string Send()
    {
        Json["deviceId"] = Random.Range(10000,100000);
        return Json.ToString();
    }

    protected override void HandleResponseImpl(JObject json)
    {
        GameLayer.I.SceneController.LoadScene("Lobby", true, null);
    }
}
