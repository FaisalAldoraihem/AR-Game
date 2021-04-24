using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameManager : GameManagerInterface, IInitializable
{
    private PuzzleSO lastSelectedMarker;
    private List<int> solvedPuzzles;
    private bool loadAR;

    protected Firebase.Auth.FirebaseAuth auth;

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
        CheckSaves();
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }

    public void LoadMainMenue()
    {
        SceneManager.LoadScene("MainMenue");
    }

    public void LoadAR()
    {
        SceneManager.LoadScene("AR");
        loadAR = true;
    }

    public void LoadMultipleChoice()
    {
        SceneManager.LoadScene("MultipleChoice");
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
        ES3.Save<List<int>>("solvedPuzzles", solvedPuzzles);
    }

    public void LoadPuzzle(PuzzleSO puzzle)
    {
        lastSelectedMarker = puzzle;
        loadAR = false;
        LoadMultipleChoice();
    }

    public void Return()
    {
        if (loadAR)
        {
            LoadAR();
        }
        else
        {
            LoadMainMenue();
        }
    }

    public string GetCurrentUser()
    {
        return auth.CurrentUser.Email;
    }

    public void SignOut()
    {
        auth.SignOut();
        SceneManager.LoadScene("Main");
    }

    private void CheckSaves()
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

    public bool CheckTotorial()
    {
        if (ES3.KeyExists("totorial"))
        {
            return ES3.Load<bool>("totorial");
        }
        else
        {
            ES3.Save<bool>("totorial", false);
            return false;
        }

    }
}
