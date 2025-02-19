using UnityEngine;
using UnityEngine.UI;

public class FinishWindow : MonoBehaviour
{
    [SerializeField] private Text pointsText;
    [SerializeField] private Button restartGameButton, menuButton;

    private void Start()
    {
        pointsText.text = SaveController.Instance.LoadPoints().ToString();

        restartGameButton.onClick.AddListener(GameController.Instance.DeleteSaveAndRestartGame);
        menuButton.onClick.AddListener(GameController.Instance.GoToMenu);
    }
}
