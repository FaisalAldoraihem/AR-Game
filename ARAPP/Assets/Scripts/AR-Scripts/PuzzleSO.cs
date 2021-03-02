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
    
    private void Awake()
    {
        CheckSolved();
    }

    public bool CheckSolved()
    {
        return ES3.Load<List<int>>("solvedPuzzles").Contains(PuzzleID);
    }
}
