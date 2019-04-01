using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthScript : MonoBehaviour
{
    public int MaxHealth = 100;
    public int CurrentHealth;

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public delegate void OnDamageTaken();
    public OnDamageTaken ITookDamage;

    public void GetDamage(int _hitPoints)
    {
        if (CurrentHealth > 0)
        {
            CurrentHealth -= _hitPoints;
        }
        ITookDamage.Invoke();
    }

    public void RecoverHealth(int _hitPoints)
    {
        if (CurrentHealth < MaxHealth)
        {
            CurrentHealth += _hitPoints;
        }
    }
}
