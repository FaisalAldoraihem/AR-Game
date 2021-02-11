using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{ 
    
    [SerializeField] GameEvents events = null;

    private Question currentQuestion;
    private AnswerData PickedAnswer;
    


    void OnEnable()
    {
        events.UpdateQuestionAnswer += UpdateAnswer;
    }
    /// <summary>
    /// Function that is called when the behaviour becomes disabled
    /// </summary>
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
        Debug.Log(currentQuestion.Info);
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
        Debug.Log("Answer is: " + isCorrect);
        var type = isCorrect ? UIManager.ResolutionScreenType.Correct : UIManager.ResolutionScreenType.Incorrect;

        if (events.DisplayResolutionScreen != null)
        {
            events.DisplayResolutionScreen(type);
            Debug.Log("Answer type: " + type);
        }
        //TODO playaudio? too taky? (its not furniture dumbass)
        //Todo update the players Answerd questions if correct (events updatedAnswerdQuestions witch will have our api class supscribed to it whenever we make it?)
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Return()
    {
        SceneManager.LoadScene("AR");
    }

    private bool CheckAnswer()
    {
        if (PickedAnswer != null)
        {
            int correctIndex = currentQuestion.GetCorrectAnswer();
            Debug.Log("Answer index: " + correctIndex);
            return PickedAnswer.AnswerIndex == correctIndex;
        }
        return false;
    }

    void LoadQuestion()
    {
        PuzzleMarkerSO puzzleMarkerSO = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().LastSelectedMarker;
        if(puzzleMarkerSO != null)
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
