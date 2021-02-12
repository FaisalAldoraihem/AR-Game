using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class PuzzleManager : MonoBehaviour
{

    [SerializeField] GameEvents events = null;

    private GameManagerInterface gmInterface;
    private Question currentQuestion;
    private AnswerData PickedAnswer;


    [Inject]
    public void Setup(GameManagerInterface GMInterface)
    {
        gmInterface = GMInterface;
    }

    void OnEnable()
    {
        events.UpdateQuestionAnswer += UpdateAnswer;
    }
   
    void OnDisable()
    {
        events.UpdateQuestionAnswer -= UpdateAnswer;
    }


    private void Awake()
    {
        SetOrientation();
    }



    private void Start()
    {
        LoadQuestion();
        Display();
    }

    void Display()
    {
        if (events.UpdateQuestionUI != null)
        {
            events.UpdateQuestionUI(currentQuestion);
        }
        else { Debug.LogWarning("GameEvents.UpdateQuestionUI is null. Issue occured in GameManager.Display() method."); }
    }

    void UpdateAnswer(AnswerData newAnswer)
    {
        if (PickedAnswer == null)
        {
            PickedAnswer = newAnswer;
        }
        else
        {
            PickedAnswer.Reset();
            PickedAnswer = newAnswer;
        }
    }

    public void Accept()
    {
        bool isCorrect = CheckAnswer();
        var type = isCorrect ? UIManager.ResolutionScreenType.Correct : UIManager.ResolutionScreenType.Incorrect;

        if (events.DisplayResolutionScreen != null)
        {
            events.DisplayResolutionScreen(type);

        }
        //TODO playaudio? too taky? (its not furniture dumbass)
        //Todo update the players Answerd questions if correct 
    }

    public void Retry()
    {
        gmInterface.RetryScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Return()
    {
        gmInterface.LoadAR();
    }

    private bool CheckAnswer()
    {
        if (PickedAnswer != null)
        {
            int correctIndex = currentQuestion.GetCorrectAnswer();
            return PickedAnswer.AnswerIndex == correctIndex;
        }
        return false;
    }

    void LoadQuestion()
    {
        PuzzleMarkerSO puzzleMarkerSO = gmInterface.GetLastSelectedMarker();
        Debug.Log(puzzleMarkerSO.name);
        if (puzzleMarkerSO != null)
        {
            Debug.Log("Marker Loaded");
            Object obj = Resources.Load<Question>("Questions/" + puzzleMarkerSO.puzzleID);
            currentQuestion = (Question)obj;
        }
    }

    private static void SetOrientation()
    {
        Screen.orientation = ScreenOrientation.Landscape;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
    }
}
