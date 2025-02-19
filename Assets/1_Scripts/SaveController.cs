using UnityEngine;

public class SaveController : Singletone<SaveController>
{
    private const string SOUND_KEY = "Sounds", MUSIC_KEY = "Music", LEVEL_KEY = "Levels", POINTS_KEY = "Points";

    public void SaveLevels(int currentLevel)
    {
        PlayerPrefs.SetInt(LEVEL_KEY, currentLevel);
        PlayerPrefs.Save();
    }

    public void SaveSoundsVolume(float value)
    {
        PlayerPrefs.SetFloat(SOUND_KEY, value);
        PlayerPrefs.Save();
    }

    public void SaveMusicVolume(float value)
    {
        PlayerPrefs.SetFloat(MUSIC_KEY, value);
        PlayerPrefs.Save();
    }

    public void SavePoints(int points)
    {
        PlayerPrefs.SetInt(POINTS_KEY, points);
        PlayerPrefs.Save();
    }


    public int LoadCurrentLevel()
    {
        return PlayerPrefs.GetInt(LEVEL_KEY, 1);
    }

    public float LoadSoundsVolume()
    {
        return PlayerPrefs.GetFloat(SOUND_KEY, 1.0f);
    }

    public float LoadMusicVolume()
    {
        return PlayerPrefs.GetFloat(MUSIC_KEY, 1.0f);
    }

    public int LoadPoints()
    {
        return PlayerPrefs.GetInt(POINTS_KEY, 0);
    }


    public void DeleteSaves()
    {
        PlayerPrefs.DeleteAll();
    }
}
