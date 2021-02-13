using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[Serializable()]
public struct UIManagerParamaters
{
    [Header("Answers Options")]
    [SerializeField] float margins;


    [Header("Resolution Screen Options")]
    [SerializeField] Color correctBGColor;
    [SerializeField] Color incorrectBGColor;

    public Color CorrectBGColor { get { return correctBGColor; } }
    public Color IncorrectBGColor { get { return incorrectBGColor; } }
    public float Margins { get { return margins; } }
}

[Serializable()]
public struct UIElements
{

    [SerializeField] [SceneObjectsOnly] RectTransform answersContentArea;
    [SerializeField] [SceneObjectsOnly] TextMeshProUGUI questionInfoTextObject;

    [Space]

    [SerializeField] [SceneObjectsOnly] DOTweenAnimation resolutionScreenAnimator;
    [SerializeField] [SceneObjectsOnly] Image resolutionBG;
    [SerializeField] [SceneObjectsOnly] TextMeshProUGUI resolutionStateInfoText;
    [SerializeField] [SceneObjectsOnly] TextMeshProUGUI resolutionHintText;

    [Space]

    [SerializeField] [SceneObjectsOnly] CanvasGroup mainCanvasGroup;
    [SerializeField] [SceneObjectsOnly] Button _return;
    [SerializeField] [SceneObjectsOnly] Button showExplanation;
    [SerializeField] [SceneObjectsOnly] Button retry;

    #region Getters
    public RectTransform AnswersContentArea { get { return answersContentArea; } }
    public TextMeshProUGUI QuestionInfoTextObject { get { return questionInfoTextObject; } }
    public DOTweenAnimation ResolutionScreenAnimator { get { return resolutionScreenAnimator; } }
    public Image ResolutionBG { get { return resolutionBG; } }
    public TextMeshProUGUI ResolutionStateInfoText { get { return resolutionStateInfoText; } }
    public TextMeshProUGUI ResolutionHintText { get { return resolutionHintText; } }
    public CanvasGroup MainCanvasGroup { get { return mainCanvasGroup; } }
    public Button ReturnWin { get { return _return; } }
    public Button ShowExplanation { get { return showExplanation; } }
    public Button Retry { get { return retry; } }
    #endregion
}
public class UIManager : MonoBehaviour
{

    private SignalBus _signalBus;

    private bool explained = false;
    private string _explanation;

    public enum ResolutionScreenType { Correct, Incorrect }

    [Title("Refrances (AssetsOnly)")]
    [SerializeField] [AssetsOnly] AnswerData answerPrefab;

    [Title("UI (Scene OBJ only)")]
    [SerializeField] [SceneObjectsOnly] UIElements uIElements;

    [Title("Paramaters")]
    [SerializeField] [SceneObjectsOnly] UIManagerParamaters parameters;

    [Inject]
    public void Setup(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void OnEnable()
    {
        _signalBus.Subscribe<QuestionUpdatedSignal>((Signal) => UpdateQuestionUI(Signal.Question));
        _signalBus.Subscribe<QuestionAnswerdSignal>((Signal) => DisplayResolution(Signal.Type));
    }

    private void OnDisable()
    {
        _signalBus.TryUnsubscribe<QuestionUpdatedSignal>((Signal) => UpdateQuestionUI(Signal.Question));
        _signalBus.TryUnsubscribe<QuestionAnswerdSignal>((Signal) => DisplayResolution(Signal.Type));
    }

    public void UpdateQuestionUI(Question question)
    {
        uIElements.QuestionInfoTextObject.text = question.Info;
        uIElements.ResolutionHintText.text = question.Hint;
        _explanation = question.Explanation;

        CreateAnswers(question);
    }

    void CreateAnswers(Question question)
    {
        float offset = 0 - parameters.Margins;
        for (int i = 0; i < question.Answers.Length; i++)
        {
            AnswerData newAnswer = Instantiate(answerPrefab, uIElements.AnswersContentArea);
            newAnswer.UpdateData(question.Answers[i].Info, i);
            newAnswer.Rect.anchoredPosition = new Vector2(0, offset);
            offset -= (newAnswer.Rect.sizeDelta.y + parameters.Margins);
        }
    }

    public void DisplayResolution(ResolutionScreenType type)
    {
        UpdateResolutionUI(type);
        uIElements.ResolutionScreenAnimator.DOPlay();
        uIElements.MainCanvasGroup.blocksRaycasts = false;
    }

    void UpdateResolutionUI(ResolutionScreenType type)
    {
        switch (type)
        {
            case ResolutionScreenType.Correct:
                uIElements.ResolutionStateInfoText.text = "Ya Beb";
                uIElements.ResolutionBG.color = parameters.CorrectBGColor;
                uIElements.ResolutionHintText.text = String.Empty;
                SwitchButtons(true);
                break;

            case ResolutionScreenType.Incorrect:
                uIElements.ResolutionStateInfoText.text = "Na Beb";
                uIElements.ResolutionBG.color = parameters.IncorrectBGColor;
                SwitchButtons(false);
                break;
        }
    }

    void SwitchButtons(bool state)
    {
        if (state)
        {
            uIElements.Retry.gameObject.SetActive(false);
            uIElements.ShowExplanation.gameObject.SetActive(true);
        }
        else
        {
            uIElements.ShowExplanation.gameObject.SetActive(false);
            uIElements.Retry.gameObject.SetActive(true);
        }
    }

    public void ShowExplanation()
    {
        if (!explained)
        {
            uIElements.ResolutionStateInfoText.DOText(_explanation, 1);
            uIElements.ResolutionStateInfoText.DOPlay();
            explained = true;
        }
    }
}