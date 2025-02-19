using System;
using UnityEngine;

public class DataCharacter : MonoBehaviour
{
    public Action<int, int> ChangeHealthEvent;
    public Action DeathEvent;

    [SerializeField] private int startHealth, startDamage;
    [SerializeField] private float startSpeedMove, startForceJump;

    [HideInInspector]
    public int CurrentHealth, MaxHealth, CurrentDamage;
    [HideInInspector]
    public float SpeedMove, ForceJump;

    public void Init()
    {
        MaxHealth = startHealth;
        CurrentHealth = MaxHealth;

        CurrentDamage = startDamage;
        SpeedMove = startSpeedMove;
        ForceJump = startForceJump;
    }

    public void GetDamage(int damage)
    {
        CurrentHealth -= damage;

        ChangeHealthEvent?.Invoke(CurrentHealth, MaxHealth);

        if (CurrentHealth <= 0)
        {
            DeathEvent?.Invoke();
        }
    }

    public void AddHealth(int value)
    {
        CurrentHealth += value;

        if(CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }

        ChangeHealthEvent?.Invoke(CurrentHealth, MaxHealth);
    }
}
