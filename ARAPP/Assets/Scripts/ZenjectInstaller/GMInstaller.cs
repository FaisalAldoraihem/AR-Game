using Zenject;

public class GMInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
        SignalBusInstaller.Install(Container);
    }
}