using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SettingsWindow : MonoBehaviour
{
    [SerializeField] private CanvasGroup substrate;
    [SerializeField] private Button openButton, closeButton, restartButton;
    [SerializeField] private Slider soundsSlider, musicSlider;

    private void Start()
    {
        openButton.onClick.AddListener(Open);
        closeButton.onClick.AddListener(Close);
        restartButton.onClick.AddListener(GameController.Instance.RestartGame);

        soundsSlider.onValueChanged.AddListener((float value) => AudioController.Instance.ChangeSoundsVolume(soundsSlider.value));
        musicSlider.onValueChanged.AddListener((float value) => AudioController.Instance.ChangeMusicVolume(musicSlider.value));
    }

    private void Open()
    {
        substrate.gameObject.SetActive(true);       
        substrate.DOFade(1, 0.3f).OnComplete(() => Time.timeScale = 0);

        soundsSlider.value = SaveController.Instance.LoadSoundsVolume();
        musicSlider.value = SaveController.Instance.LoadMusicVolume();
    }

    private void Close()
    {
        substrate.DOFade(0, 0.3f).OnComplete(() => substrate.gameObject.SetActive(false));
        
        Time.timeScale = 1;

        SaveController.Instance.SaveSoundsVolume(soundsSlider.value);
        SaveController.Instance.SaveMusicVolume(musicSlider.value);
    }
}
