using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    int playerMoney = 0;

    private void OnEnable()
    {
        Basic_ItemReward.EarnMoney += EarnPlayerMoney;
    }

    private void OnDisable()
    {
        Basic_ItemReward.EarnMoney -= EarnPlayerMoney;
    }

    public int GetPlayerMoney()
    {
        return playerMoney;
    }

    public void EarnPlayerMoney(int money)
    {
        playerMoney += money;
    }

}
