using Zenject;

public class ARSignalsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.DeclareSignal<MarkerScannedSignal>();
        Container.BindSignal<MarkerScannedSignal>().ToMethod<GameManager>
            ((GM, MarkerSignal) => GM.SetLastSelectedMarker(MarkerSignal.Marker)).FromResolve();
    }
}