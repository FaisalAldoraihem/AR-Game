

public interface GameManagerInterface
{
    void LoadMainMenue();
    void LoadAR();
    void LoadMultipleChoice();
    void Quit();
    PuzzleSO GetLastSelectedMarker();

    void RetryScene(int sceneID);
}
