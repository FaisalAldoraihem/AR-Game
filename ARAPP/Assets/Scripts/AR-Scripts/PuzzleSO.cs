using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PuzzleSO", order = 1)]
public class PuzzleSO : ScriptableObject
{
    public int puzzleID;

    public string puzzleTitle;

    public string puzzleDescription;

}
