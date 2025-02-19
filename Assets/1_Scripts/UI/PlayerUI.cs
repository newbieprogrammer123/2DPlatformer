using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Text counterCrystalsText;
    [SerializeField] private Text counterCrownText;

    public void ChangeHealthSlider(int health, int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
    }

    public void ChangeCounterCrystalsText(int currentValue, int needValue)
    {
        counterCrystalsText.text = $"{currentValue}/{needValue}";
    }

    public void ChangeCounterCrownText(int currentValue, int needValue)
    {
        counterCrownText.text = $"{currentValue}/{needValue}";
    }
}
