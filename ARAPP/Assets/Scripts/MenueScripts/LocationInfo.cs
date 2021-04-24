using UnityEngine.UI;
using UnityEngine;
using Sirenix.OdinInspector;

public class LocationInfo : MonoBehaviour
{
    [Title("UI Elements")]
    [SerializeField] Image solvedSprite;
    [SerializeField] Image locationImage;

    [Title("Refrances (AssetsOnly)")]
    [SerializeField] [AssetsOnly] PuzzleSO puzzle;
    MainMenueManager mainMenueManager;

    private void Awake()
    {
        locationImage.sprite = puzzle.locationImage;
        if (puzzle.CheckSolved())
        {
            solvedSprite.gameObject.SetActive(true);
        }
        mainMenueManager = GameObject.FindGameObjectWithTag("MainMenueManager").GetComponent<MainMenueManager>();
    }

    public void SetLastSelectedPuzzle()
    {
        mainMenueManager.SetLastSelectedPuzzle(puzzle);
    }

}
