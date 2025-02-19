using UnityEngine;

public class AudioController : Singletone<AudioController>
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource[] soundsSource;

    [SerializeField]
    private AudioClip playerHip, playerJump, playerMove,
        playerDeath, enemyHit, enemyDeath, chestOpen, takeItem, clickButton,
        playerAttack, finish;

    private void Start()
    {
        ChangeMusicVolume(SaveController.Instance.LoadMusicVolume());
        ChangeSoundsVolume(SaveController.Instance.LoadSoundsVolume());
    }

    public void PlaySound(TypeSound typeSound)
    {
        AudioClip clip;

        switch (typeSound)
        {   
            case TypeSound.PlayerHit:
                clip = playerHip;
                break;
            case TypeSound.PlayerJump:
                clip = playerJump;
                break;
            case TypeSound.PlayerMove:
                clip = playerMove;
                break;
            case TypeSound.PlayerDeath:
                clip = playerDeath;
                break;
            case TypeSound.EnemyHit:
                clip = enemyHit;
                break;
            case TypeSound.EnemyDeath:
                clip = enemyDeath;
                break;
            case TypeSound.ChestOpen:
                clip = chestOpen;
                break;
            case TypeSound.TakeItem:
                clip = takeItem;
                break;
            case TypeSound.ClickButton:
                clip = clickButton;
                break;
            case TypeSound.PlayerAttack:
                clip = playerAttack;
                break;
            case TypeSound.Finish:
                clip = finish;
                break;
            default:
                clip = clickButton;
                break;
        }

        AudioSource source = soundsSource[0];

        foreach (var src in soundsSource)
        {
            if (!src.isPlaying)
            {
                source = src;
                break;
            }
        }

        source.clip = clip;
        source.Play();
    }

    public void ChangeSoundsVolume(float value)
    {
        foreach (var source in soundsSource)
        {
            source.volume = value;
        }
    }

    public void ChangeMusicVolume(float value)
    {
        musicSource.volume = value;
    }
}

public enum TypeSound
{
    PlayerHit,
    PlayerJump,
    PlayerMove,
    PlayerDeath,
    PlayerAttack,
    EnemyHit,
    EnemyDeath,
    ChestOpen,
    TakeItem,
    ClickButton,
    Finish
}