using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenueManager : MonoBehaviour
{
    private GameManagerInterface gmInterface;
    private PuzzleSO lastSelectedPuzzle;

    #region UI
    [Title("UI Elements")]
    [SerializeField] [SceneObjectsOnly] TextMeshProUGUI puzzleLocationTitle;
    [SerializeField] [SceneObjectsOnly] TextMeshProUGUI puzzleLocationDescription;
    [SerializeField] [SceneObjectsOnly] DOTweenAnimation locationInfoPopUpAnimator;
    [SerializeField] [SceneObjectsOnly] DOTweenAnimation locationsPopUpAnimator;
    [SerializeField] [SceneObjectsOnly] DOTweenAnimation totorialPopUpAnimator;
    [SerializeField] [SceneObjectsOnly] Button locationButton;
    [SerializeField] [SceneObjectsOnly] Button locationButtonSmall;
    [SerializeField] [SceneObjectsOnly] Button replayButton;

    #endregion

    [Inject]
    public void Setup(GameManagerInterface GMInterface)
    {
        gmInterface = GMInterface;
    }
    private void Awake()
    {
        SetOrientation();
    }
    private void Start()
    {
        Totorial();
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
        SetLocationButton();
        locationsPopUpAnimator.DORestartById("CloseLocations");
        locationInfoPopUpAnimator.DORestartById("PopLocationInfo");
    }

    private void SetLocationButton()
    {

        if (lastSelectedPuzzle.CheckSolved() || AdminSignedIn())
        {
            replayButton.gameObject.SetActive(true);
            locationButtonSmall.gameObject.SetActive(true);
            locationButton.gameObject.SetActive(false);
        }
        else
        {
            locationButton.gameObject.SetActive(true);
            replayButton.gameObject.SetActive(false);
            locationButtonSmall.gameObject.SetActive(false);
        }
    }

    public void ReplayPuzzle()
    {
        gmInterface.LoadPuzzle(lastSelectedPuzzle);
    }

    public void SignOut()
    {
        gmInterface.SignOut();
    }

    private bool AdminSignedIn()
    {
        return gmInterface.GetCurrentUser() == "faisalaldoraihem@gmail.com";
    }

    private static void SetOrientation()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Screen.autorotateToPortrait = true;
        Screen.autorotateToPortraitUpsideDown = true;
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
    }

    public void OpenLocation()
    {
        Application.OpenURL(lastSelectedPuzzle.locationLink);
    }

    private void Totorial()
    {
        if (!gmInterface.CheckTotorial())
        {
            totorialPopUpAnimator.DORestartById("PoPTotorial");
            ES3.Save<bool>("totorial", true);
        }
    }
}
