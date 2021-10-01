using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour
{
    public static Action<int> ShowMoney;

    [SerializeField]
    static int playerMoney = 0;

    //================================================

    /// <summary>
    /// Testeo de Daño
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            EarnPlayerMoney(1);
            Debug.Log("Se Sumo Dinero Magicamente!");
        }
    }
    /// </summary>
    /// 

    private void OnEnable()
    {
        Basic_ItemReward.EarnMoney += EarnPlayerMoney;
        MarketSlot.PayItem += LosePlayerMoney;
    }

    private void OnDisable()
    {
        Basic_ItemReward.EarnMoney -= EarnPlayerMoney;
        MarketSlot.PayItem -= LosePlayerMoney;
    }

    //================================================

    public static int GetPlayerMoney()
    {
        return playerMoney;
    }

    public void EarnPlayerMoney(int money)
    {
        playerMoney += money;

        ShowMoney(GetPlayerMoney());
    }

    public void LosePlayerMoney(int cost)
    {
        playerMoney -= cost;

        ShowMoney(GetPlayerMoney());
    }
}
