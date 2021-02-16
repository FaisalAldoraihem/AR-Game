using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : GameManagerInterface
{
    private PuzzleSO lastSelectedMarker;

    public void LoadMainMenue()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadAR()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMultipleChoice()
    {
        SceneManager.LoadScene(2);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public PuzzleSO GetLastSelectedMarker()
    {
        return lastSelectedMarker;
    }

    public void SetLastSelectedMarker(PuzzleSO marker)
    {
        Debug.Log(marker.name);
        this.lastSelectedMarker = marker;
    }

    public void RetryScene(int SceneID)
    {
        SceneManager.LoadScene(SceneID);
    }
}
