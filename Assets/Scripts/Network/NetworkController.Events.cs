public partial class NetworkController
{

    public void Login()
    {
        Send(_eventAgregator.GetEvent<LoginEvent>().Send());
    }

    public void PlayGame()
    {
        Send(_eventAgregator.GetEvent<PlayGameEvent>().Send());
    }

}
