using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameEvent", order = 3)]
public class GameEvents : ScriptableObject
{
    public delegate void UpdateQuestionUICallback(Question question);
    public UpdateQuestionUICallback UpdateQuestionUI = null;

    public delegate void UpdateQuestionAnswerCallback(AnswerData pickedAnswer);
    public UpdateQuestionAnswerCallback UpdateQuestionAnswer = null;

    public delegate void updateAnswerdQuestions(int questionID);
    public updateAnswerdQuestions questionsUpdated = null;

    public delegate void DisplayResolutionScreenCallback(UIManager.ResolutionScreenType type);
    public DisplayResolutionScreenCallback DisplayResolutionScreen = null;

    public delegate void SetLastSelectedMarker(PuzzleMarkerSO marker);
    public SetLastSelectedMarker setLastSelectedMarker = null; 

}
