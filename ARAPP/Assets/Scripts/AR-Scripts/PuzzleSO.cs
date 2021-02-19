using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PuzzleSO", order = 1)]
public class PuzzleSO : ScriptableObject
{
    public int PuzzleID;

    public string PuzzleTitle;

    public string PuzzleDescription;

    public string LocationName;

    public string LocationDesctiption;

    private bool solved;
    public bool Solved { get { return solved; }  }

    private void Awake()
    {
        CheckSolved();
    }

    private void CheckSolved()
    {
        if (ES3.KeyExists("solvedPuzzles"))
        {
            solved = ES3.Load<List<int>>("solvedPuzzles").Contains(PuzzleID);
        }
        else
        {
            solved = false;
        }
    }
}
