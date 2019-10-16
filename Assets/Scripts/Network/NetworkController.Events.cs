using System.Collections.Generic;

public partial class NetworkController
{

    public void SendLogin()
    {
        Send(_eventAgregator.GetEvent<LoginEvent>().Send());
    }

    public void SendPlayGame()
    {
        Send(_eventAgregator.GetEvent<PlayGameEvent>().Send());
    }

    public void SendPlayerTurn(List<ActionPhase> turnData)
    {
        Send(_eventAgregator.GetEvent<PlayerTurnEvent>().SetData(turnData));
    }
}
