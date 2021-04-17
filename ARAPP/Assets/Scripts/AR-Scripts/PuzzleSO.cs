using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PuzzleSO", order = 1)]
public class PuzzleSO : ScriptableObject
{
    public int PuzzleID;

    public string PuzzleTitle;

    [TextArea(10, 100)] public string PuzzleDescription;

    public string LocationName;

    [TextArea(10, 100)] public string LocationDesctiption;    
    private void Awake()
    {
        CheckSolved();
    }

    public bool CheckSolved()
    {
        if (ES3.KeyExists("solvedPuzzles"))
        {
            return ES3.Load<List<int>>("solvedPuzzles").Contains(PuzzleID);
        }
        else
        {
            return false;
        }
    }
}
