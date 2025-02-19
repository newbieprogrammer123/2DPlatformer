 using UnityEngine.SceneManagement;

public class MenuWindow : Singletone<MenuWindow>
{
    private LevelButton[] levelButtons;

    private void Start()
    {
        levelButtons = GetComponentsInChildren<LevelButton>();

        int countLevels = SceneManager.sceneCountInBuildSettings;

        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].Init(i + 1, i + 1 <= SaveController.Instance.LoadCurrentLevel());
        }
    }

    public void StartLevel(int numberLevel)
    {
        SceneManager.LoadScene(numberLevel);
    }
}
