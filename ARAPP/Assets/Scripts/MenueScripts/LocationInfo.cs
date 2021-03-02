using UnityEngine.UI;
using UnityEngine;
using Sirenix.OdinInspector;

public class LocationInfo : MonoBehaviour
{
    [Title("UI Elements")]
    [SerializeField] [AssetsOnly] Sprite solvedSprite = null;
    [SerializeField] Image icon;

    [Title("Refrances (AssetsOnly)")]
    [SerializeField] [AssetsOnly] PuzzleSO puzzle;
    [SerializeField] [SceneObjectsOnly] MainMenueManager mainMenueManager;

    private void Awake()
    {
        if (puzzle.CheckSolved())
        {
            Debug.Log("Sloved: LocationInfo");
            icon.sprite = solvedSprite;
        }
        if (mainMenueManager == null)
        {
            mainMenueManager = GameObject.FindGameObjectWithTag("MainMenueManager").GetComponent<MainMenueManager>();
        }
    }

    public void SetLastSelectedPuzzle()
    {
        mainMenueManager.SetLastSelectedPuzzle(puzzle);   
    }

}
