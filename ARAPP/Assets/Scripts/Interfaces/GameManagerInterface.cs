

public interface GameManagerInterface
{
    void LoadMainMenue();
    void LoadAR();
    void LoadMultipleChoice();
    void Quit();
    PuzzleSO GetLastSelectedMarker();

    void RetryScene(int sceneID);
    void SaveAnswerdQuestion(int questionID);

    void LoadPuzzle(PuzzleSO puzzle);

    void Return();

    string GetCurrentUser();

    void SignOut();
}
