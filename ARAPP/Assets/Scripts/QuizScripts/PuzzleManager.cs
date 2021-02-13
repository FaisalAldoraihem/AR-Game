using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class PuzzleManager : MonoBehaviour
{
    private GameManagerInterface gmInterface;
    private SignalBus _signalBus;

    private Question currentQuestion;
    private AnswerData PickedAnswer;


    [Inject]
    public void Setup(GameManagerInterface GMInterface, SignalBus signalBus)
    {
        gmInterface = GMInterface;
        _signalBus = signalBus;
    }

    private void Awake()
    {
        SetOrientation();
    }
    private void OnEnable()
    {
        _signalBus.Subscribe<AnswerPickedSignal>((Signal) => UpdateAnswer(Signal.Answer));
    }

    private void OnDisable()
    {
        _signalBus.Subscribe<AnswerPickedSignal>((Signal) => UpdateAnswer(Signal.Answer));
    }


    private void Start()
    {
        LoadQuestion();
        Display();
    }

    void Display()
    {
        if (currentQuestion != null)
        {
            _signalBus.Fire(new QuestionUpdatedSignal(currentQuestion));
        }
        else
        {
            Debug.LogWarning("The Current Question is null. Issue occured in PuzzleManager.Display() method.");
        }
    }

    public void UpdateAnswer(AnswerData newAnswer)
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

    //TODO playaudio? too taky? (its not furniture dumbass)
    //TODO update the players Answerd questions if correct 
    public void Accept()
    {
        bool isCorrect = CheckAnswer();
        var type = isCorrect ? UIManager.ResolutionScreenType.Correct : UIManager.ResolutionScreenType.Incorrect;

        _signalBus.Fire(new QuestionAnswerdSignal(type));
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
        if (puzzleMarkerSO != null)
        {
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
