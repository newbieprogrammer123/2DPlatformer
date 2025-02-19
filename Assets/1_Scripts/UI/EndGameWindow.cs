using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EndGameWindow : MonoBehaviour
{
    [SerializeField] private GameObject substrate;
    [SerializeField] private Text endGameText;
    [SerializeField] private Button restartButton, menuButton, nextLevelButton;

    private void Start()
    {
        restartButton.onClick.AddListener(() =>
        {
            GameController.Instance.RestartGame();
            Time.timeScale = 1;
        });

        menuButton.onClick.AddListener(() =>
        {
            GameController.Instance.GoToMenu();
            Time.timeScale = 1;            
        });

        nextLevelButton.onClick.AddListener(() =>
        {
            GameController.Instance.NextLevel();
            Time.timeScale = 1;
        });
    }

    public void ShowWindow(bool isWin)
    {
        if (isWin)
        {
            endGameText.text = "You win!";
        }
        else
        {
            endGameText.text = "You lose!";
        }

        restartButton.gameObject.SetActive(!isWin);
        nextLevelButton.gameObject.SetActive(isWin);

        substrate.SetActive(true);
        substrate.transform.DOLocalMoveX(0, 0.3f).OnComplete(() => Time.timeScale = 0);
    }
}
