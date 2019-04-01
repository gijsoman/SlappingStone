using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region !!Singleton!!
    public static GameManager Instance { get; private set; }
    [System.NonSerialized] public int Value;
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public HealthScript playerHealth;

    private void Start()
    {
        playerHealth.ITookDamage += CheckHealth;
    }

    public void CheckHealth()
    {
        if (playerHealth.CurrentHealth <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
    }
}
