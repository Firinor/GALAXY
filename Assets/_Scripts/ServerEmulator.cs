using System.Threading;

public class ServerEmulator
{
    const int PING = 1300;

    public CameraSetting GetCameraSetting()
    {
        //Emulation of the signal delay from the server
        Thread.Sleep(PING);

        return new CameraSetting
        {
            MinZoom = .1f,
            ZoomStep = 0.15f,
            MaxZoom = 4.5f,
        };
    }
}
