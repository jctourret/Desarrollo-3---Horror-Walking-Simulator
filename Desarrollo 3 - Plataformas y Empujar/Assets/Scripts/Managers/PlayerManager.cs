using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour
{
    public static Action<int> ShowMoney;

    [SerializeField]
    int playerMoney = 0;

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
    }

    private void OnDisable()
    {
        Basic_ItemReward.EarnMoney -= EarnPlayerMoney;
    }

    //================================================

    public int GetPlayerMoney()
    {
        return playerMoney;
    }

    public void EarnPlayerMoney(int money)
    {
        playerMoney += money;

        ShowMoney(GetPlayerMoney());
    }

}
