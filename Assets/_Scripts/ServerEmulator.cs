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
            MinZoom = .01f,
            ZoomStep = 0.05f,
            MaxZoom = 4.5f,
            GalaxyEdge = 4.5f,
        };
    }
}
