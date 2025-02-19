using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    private int numberLevel;

    public void Init(int number, bool isActive)
    {
        numberLevel = number;

        GetComponentInChildren<Text>().text = numberLevel.ToString();
        GetComponent<Button>().onClick.AddListener(() => MenuWindow.Instance.StartLevel(numberLevel));
        GetComponent<Button>().interactable = isActive;
    }
}
