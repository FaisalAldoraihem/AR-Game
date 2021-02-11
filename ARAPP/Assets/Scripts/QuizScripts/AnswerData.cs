using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AnswerData : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] TextMeshProUGUI infoTextObject;
    [SerializeField] Image toggle = null;

    [Header("Textures")]
    [SerializeField] Sprite uncheckedToggle = null;
    [SerializeField] Sprite checkedToggle = null;

    [Header("References")]
    [SerializeField] GameEvents events = null;

    private RectTransform _rect;
    public RectTransform Rect
    {
        get
        {
            if (_rect == null)
            {
                _rect = GetComponent<RectTransform>() ?? gameObject.AddComponent<RectTransform>();
            }
            return _rect;
        }
    }
    private int _anserIndex = -1;
    public int AnswerIndex { get { return _anserIndex; } }

    private bool cheacked = false;

    public void UpdateData(string info, int index)
    {
        infoTextObject.text = info;
        _anserIndex = index;
    }

    public void Reset()
    {
        cheacked = false;
        UpdateUI();
    }

    public void SwitchState()
    {
        cheacked = !cheacked;
        UpdateUI();

        if (events.UpdateQuestionAnswer != null)
        {
            events.UpdateQuestionAnswer(this);
        }
    }
    void UpdateUI()
    {
        toggle.sprite = (cheacked) ? checkedToggle : uncheckedToggle;
    }
}
