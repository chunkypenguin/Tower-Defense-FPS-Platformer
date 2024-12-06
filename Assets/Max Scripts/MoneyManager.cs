using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int playerMoney = 100; // Starting money

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ChangeMoney(int amount)
    {
        playerMoney += amount;
    }

    public bool CanAfford(int price)
    {
        return playerMoney >= price;
    }
}
