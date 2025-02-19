using UnityEngine;
using UnityEngine.UI;

public class ClickButtonSound : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => AudioController.Instance.PlaySound(TypeSound.ClickButton));
    }
}
