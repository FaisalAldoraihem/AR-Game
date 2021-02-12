

public interface GameManagerInterface
{
    void LoadMainMenue();
    void LoadAR();
    void LoadMultipleChoice();
    void Quit();
    PuzzleMarkerSO GetLastSelectedMarker();

    void RetryScene(int sceneID);
}
