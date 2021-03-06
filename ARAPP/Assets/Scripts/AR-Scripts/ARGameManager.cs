
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Zenject;

public class ARGameManager : MonoBehaviour
{

    private GameManagerInterface gmInterface;
    private SignalBus _signalBus;

    private bool poped = false;

    [Title("UI (Scene OBJ only)")]
    [SerializeField] [SceneObjectsOnly] Camera arCamera;
    [SerializeField] [SceneObjectsOnly] DOTweenAnimation popUpAnimator;
    [SerializeField] [SceneObjectsOnly] TextMeshProUGUI puzzleTitle;
    [SerializeField] [SceneObjectsOnly] TextMeshProUGUI puzzleDescription;

    public DOTweenAnimation PopUpAnimator { get { return popUpAnimator; } }

    [Inject]
    public void Setup(GameManagerInterface GMInterface, SignalBus signalBus)
    {
        gmInterface = GMInterface;
        _signalBus = signalBus;
    }


    private void Awake()
    {
        SetOrentation();
    }

    private void SetOrentation()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Screen.autorotateToPortrait = true;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
    }

    void Update()
    {

        if (Input.touchCount > 0 && !poped)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    PuzzleMarker marker = hit.collider.gameObject.GetComponent<PuzzleMarker>();
                    if (marker != null)
                    {
                        MarkerSelected(marker);
                    };
                }
            }

        }

        if (Input.GetMouseButtonDown(0) && !poped)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitObject))
            {
                Debug.Log("It has the marker");
                PuzzleMarker marker = hitObject.collider.gameObject.GetComponent<PuzzleMarker>();
                if (marker != null)
                {
                    MarkerSelected(marker);
                };
            }
        }
    }

    public void PopUp(PuzzleSO puzzle)
    {
        puzzleTitle.text = puzzle.PuzzleTitle;
        puzzleDescription.text = puzzle.PuzzleDescription;
        PopUpAnimator.DORestartById("PopUP");
        poped = true;
    }

    public void ClosePopUp()
    {
        PopUpAnimator.DORestartById("Close");
        poped = false;
    }

    void MarkerSelected(PuzzleMarker marker)
    {
        PuzzleSO markerSo = marker.PuzzleSO;
        PopUp(markerSo);
        _signalBus.Fire(new MarkerScannedSignal(markerSo));
    }

    public void LoadMultipleChoice()
    {
        gmInterface.LoadMultipleChoice();
    }

    //Add a popup to make sure of the exit action beb
    public void LoadMainMenue()
    {
        if (!poped)
        {
            gmInterface.LoadMainMenue();
        }

    }
}
