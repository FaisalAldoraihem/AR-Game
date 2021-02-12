using UnityEngine;
using Zenject;

public class MainMenueManager : MonoBehaviour
{
    private GameManagerInterface gmInterface;

    [Inject]
    public void Setup(GameManagerInterface GMInterface)
    {
        gmInterface = GMInterface;
    }

    public void Play()
    {
        gmInterface.LoadAR();
    }

    public void Quit()
    {
        gmInterface.Quit();
    }
}
