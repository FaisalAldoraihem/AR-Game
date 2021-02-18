using UnityEngine;
using Sirenix.OdinInspector;

public class PuzzleMarker : MonoBehaviour
{
    [SerializeField] [AssetsOnly] private PuzzleSO puzzleSO;
    public PuzzleSO PuzzleSO { get => puzzleSO; private set => puzzleSO = value; }


    //TODO: Change the UI based on wether or not the puzzle was finished 
}
