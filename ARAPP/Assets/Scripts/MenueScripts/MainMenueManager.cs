using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Zenject;

public class MainMenueManager : MonoBehaviour
{
    private GameManagerInterface gmInterface;
    private PuzzleSO lastSelectedPuzzle;

    [Title("UI Elements")]
    [SerializeField] [SceneObjectsOnly] TextMeshProUGUI puzzleLocationTitle;
    [SerializeField] [SceneObjectsOnly] TextMeshProUGUI puzzleLocationDescription;
    [SerializeField] [SceneObjectsOnly] DOTweenAnimation locationInfoPopUpAnimator;
    [SerializeField] [SceneObjectsOnly] DOTweenAnimation locationsPopUpAnimator;

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

    public void SetLastSelectedPuzzle(PuzzleSO selectedPuzzle)
    {
        lastSelectedPuzzle = selectedPuzzle;
        Debug.Log("PuzzleSet: " + lastSelectedPuzzle.LocationName);
        UpdateLocationInfo();
    }

    private void UpdateLocationInfo()
    {
        puzzleLocationTitle.text = lastSelectedPuzzle.LocationName;
        puzzleLocationDescription.text = lastSelectedPuzzle.LocationDesctiption;
        locationsPopUpAnimator.DORestartById("CloseLocations");
        locationInfoPopUpAnimator.DORestartById("PopLocationInfo");
    }
}
