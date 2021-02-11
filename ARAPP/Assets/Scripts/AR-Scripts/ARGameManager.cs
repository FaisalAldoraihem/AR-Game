
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ARGameManager : MonoBehaviour
{
    

    [Title("Refrances (AssetsOnly)")]
    [SerializeField] [AssetsOnly] GameEvents events = null;

    [Title("UI (Scene OBJ only)")]
    [SerializeField] [SceneObjectsOnly] Camera arCamera;
    [SerializeField] [SceneObjectsOnly] DOTweenAnimation popUpAnimator;
    [SerializeField] [SceneObjectsOnly] TextMeshProUGUI puzzleTitle;
    [SerializeField] [SceneObjectsOnly] TextMeshProUGUI puzzleDescription;
    
    public DOTweenAnimation PopUpAnimator { get { return popUpAnimator; } }

    private void Awake()
    {
        SetOrentation();

    }

    private static void SetOrentation()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Screen.autorotateToPortrait = true;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
    }

    void Update()
    {
        
        if (Input.touchCount > 0 )
        {
            Touch touch = Input.GetTouch(0);
            Debug.Log("Touch registerd");
            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("Touch phase begins");
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out RaycastHit hitObject))
                {
                    Debug.Log("It has the marker");
                    PuzzleMarker marker = hitObject.transform.GetComponent<PuzzleMarker>();
                    if (marker != null)
                    {
                        MarkerSelected(marker);
                    };
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitObject))
            {
                Debug.Log("It has the marker");
                PuzzleMarker marker = hitObject.transform.GetComponent<PuzzleMarker>();
                if (marker != null)
                {
                    MarkerSelected(marker);
                };
            }
        }
    }

    public void PopUp(PuzzleMarkerSO puzzle)
    {
        puzzleTitle.text = puzzle.puzzleTitle;
        puzzleDescription.text = puzzle.puzzleDescription;
        PopUpAnimator.DORestartById("PopUP");
        
    }

    public void ClosePopUp()
    {
        PopUpAnimator.DORestartById("Close");
    }

    void MarkerSelected(PuzzleMarker marker)
    {
        PuzzleMarkerSO markerSo = marker.markerSo;
        PopUp(markerSo);
        if (events.setLastSelectedMarker != null)
        {
            events.setLastSelectedMarker(markerSo);
        }
    }

    public void LoadMultipleChoice()
    {
        GameManager.Instance.LoadMultipleChoice();
    }

    public void LoadMainMenue()
    {
        GameManager.Instance.LoadMainMenue();
    }
}
