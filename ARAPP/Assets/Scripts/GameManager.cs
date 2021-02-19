using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameManager : GameManagerInterface, IInitializable
{
    private PuzzleSO lastSelectedMarker;
    private List<int> solvedPuzzles;

    #region GettersAndSetters
    public PuzzleSO GetLastSelectedMarker()
    {
        return lastSelectedMarker;
    }

    public void SetLastSelectedMarker(PuzzleSO marker)
    {
        Debug.Log(marker.name);
        this.lastSelectedMarker = marker;
    }

    public List<int> SolvedPuzzles { get { return solvedPuzzles; } }
    #endregion

    public void Initialize()
    {
        if (!ES3.KeyExists("solvedPuzzles"))
        {
            solvedPuzzles = new List<int>();
            ES3.Save<List<int>>("solvedPuzzles",solvedPuzzles);
        }
        else
        {
            solvedPuzzles = ES3.Load<List<int>>("solvedPuzzles");
        }
    }

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

    public void RetryScene(int SceneID)
    {
        SceneManager.LoadScene(SceneID);
    }
}
