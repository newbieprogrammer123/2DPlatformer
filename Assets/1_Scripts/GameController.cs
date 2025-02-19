using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : Singletone<GameController>
{
    [SerializeField] private Player player;
    [SerializeField] private EndGameWindow endGameWindow;
    [SerializeField] private int needCountCrystals;
    [SerializeField] private int needCountCrown;

    public int GetNeedCrystals
    {
        get => needCountCrystals;
    }

    public int GetNeedCrown
    {
        get => needCountCrown;
    }

    public Player GetPlayer
    {
        get { return player; }
    }

    public void PlayerDeath()
    {
        endGameWindow.ShowWindow(false);
    }

    public void FinishLevel()
    {
        SaveController.Instance.SaveLevels(SceneManager.GetActiveScene().buildIndex + 1);

        if (IsFinishGame())
        {
            Instantiate(Resources.Load("FinishWindow"));
        }
        else
        {
            endGameWindow.ShowWindow(true);
        }
    }

    private bool IsFinishGame()
    {
        return SceneManager.GetActiveScene().buildIndex + 1 >= SceneManager.sceneCountInBuildSettings;
    }

    public void RestartGame()
    {
        int numberScene = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1;
        SceneManager.LoadScene(numberScene);
    }

    public void NextLevel()
    {
        int numberScene = SceneManager.GetActiveScene().buildIndex + 1;

        if (SceneManager.sceneCountInBuildSettings < numberScene)
        {
            GoToMenu();
        }
        else
        {
            SceneManager.LoadScene(numberScene);
        }
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void DeleteSaveAndRestartGame()
    {
        SaveController.Instance.DeleteSaves();

        GoToMenu();
    }
}
