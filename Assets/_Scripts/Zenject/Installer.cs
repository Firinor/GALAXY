using Zenject;

public class Installer : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ServerEmulator>().AsSingle();

        Container.Bind<CameraController>().AsSingle();
    }
}