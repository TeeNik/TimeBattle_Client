using Newtonsoft.Json.Linq;
using UnityEngine;

public class LoginEvent : BaseEventClass
{
    public LoginEvent() : base(EventNames.Login)
    {
    }

    public string Send()
    {
        var json = new JObject();
        json["cmd"] = EventNames.Login;
        json["deviceId"] = SystemInfo.deviceUniqueIdentifier;
        return json.ToString();
    }

    protected override void HandleResponseImpl(JObject json)
    {
        GameLayer.I.SceneController.LoadScene("Lobby", true, null);
    }
}
