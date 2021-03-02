using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameManager : GameManagerInterface, IInitializable
{
    private PuzzleSO lastSelectedMarker;
    private List<int> solvedPuzzles;
    private bool loadAR;

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
        if (ES3.KeyExists("solvedPuzzles"))
        {
            solvedPuzzles = ES3.Load<List<int>>("solvedPuzzles");
        }
        else
        {
            solvedPuzzles = new List<int>();
            ES3.Save<List<int>>("solvedPuzzles", solvedPuzzles);
        }
    }

    public void LoadMainMenue()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadAR()
    {
        SceneManager.LoadScene(1);
        loadAR = true;
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

    public void SaveAnswerdQuestion(int questionID)
    {
        SolvedPuzzles.Add(questionID);
        ES3.Save<List<int>>("solvedPuzzles",solvedPuzzles);
    }

    public void LoadPuzzle(PuzzleSO puzzle){
        lastSelectedMarker = puzzle;
        loadAR = false;
        LoadMultipleChoice();
    }

    public void Return(){
        if(loadAR){
            LoadAR();
        }else{
            LoadMainMenue();
        }
    }
    

}
