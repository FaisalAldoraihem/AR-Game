using Zenject;

public class GMInstaller : MonoInstaller
{
    public override void InstallBindings()
    {

        Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<MarkerScannedSignal>();
        Container.BindSignal<MarkerScannedSignal>().ToMethod<GameManager>
            ((GM, MarkerSignal) => GM.SetLastSelectedMarker(MarkerSignal.Marker)).FromResolve();
    }
}