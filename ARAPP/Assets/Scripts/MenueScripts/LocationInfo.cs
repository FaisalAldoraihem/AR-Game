using UnityEngine.UI;
using UnityEngine;
using Sirenix.OdinInspector;

public class LocationInfo : MonoBehaviour
{
    [Title("UI Elements")]
    [SerializeField] [AssetsOnly] Sprite solvedSprite = null;
    [SerializeField] [SceneObjectsOnly] Image icon;

    [Title("Refrances (AssetsOnly)")]
    [SerializeField] [AssetsOnly] PuzzleSO puzzle;


    private void Awake()
    {
        if (puzzle.Solved)
        {
            icon.sprite = solvedSprite;
        }
    }
}
