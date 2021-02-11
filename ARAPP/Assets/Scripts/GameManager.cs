using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameEvents events = null;
    PuzzleMarkerSO lastSelectedMarker;
    public PuzzleMarkerSO LastSelectedMarker {get{return lastSelectedMarker;}}

    static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    void Awake()
    {
        if (_instance == null)
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        events.setLastSelectedMarker += SetMarker;
    }

 

    private void OnDestroy()
    {
        events.setLastSelectedMarker -= SetMarker;
    }
    public void LoadMainMenue()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadAR()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMultipleChoice()
    {
        SceneManager.LoadScene(2);
    }

    void SetMarker(PuzzleMarkerSO marker)
    {
        lastSelectedMarker = marker;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
