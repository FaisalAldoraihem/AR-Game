using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class AnswerData : MonoBehaviour
{
    private int _anserIndex = -1;
    public int AnswerIndex { get { return _anserIndex; } }

    private bool cheacked = false;
    [Title("UI Elements")]
    [SerializeField] TextMeshProUGUI infoTextObject;
    [SerializeField] Image toggle = null;

    [Title("Textures")]
    [SerializeField] Sprite uncheckedToggle = null;
    [SerializeField] Sprite checkedToggle = null;

    [Title("My dumbass is tierd")]
    [SerializeField] PuzzleManager puzzleManager;

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
    private void Start() => puzzleManager = GameObject.FindGameObjectWithTag("PuzzleManager").GetComponent<PuzzleManager>();
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
        puzzleManager.UpdateAnswer(this);
    }

    void UpdateUI()
    {
        toggle.sprite = (cheacked) ? checkedToggle : uncheckedToggle;
    }
}
