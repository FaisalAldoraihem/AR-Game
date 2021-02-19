using Sirenix.OdinInspector;
using UnityEngine;

public class PuzzleMarker : MonoBehaviour
{
    [SerializeField] [AssetsOnly] private PuzzleSO puzzleSO;
    public PuzzleSO PuzzleSO { get => puzzleSO; private set => puzzleSO = value; }
}
