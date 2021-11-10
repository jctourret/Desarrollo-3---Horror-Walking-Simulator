using UnityEngine;
using System;

public class Basic_ItemReward : Collectable
{
    public static Action<int> EarnMoney;

    enum RewardType
    {
        LifeReward,
        MoneyReward,
    };

    [SerializeField] RewardType rewardType;
    [SerializeField] int earnScore = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            switch (rewardType)
            {
                case RewardType.LifeReward:
                    if (other.transform.GetComponentInParent<PlayerStats>().EarnLive(earnScore))
                    {
                        Destroy(this.gameObject);
                    }
                    break;

                case RewardType.MoneyReward:
                    EarnMoney(earnScore);
                    Destroy(this.gameObject);
                    break;
            }

            AkSoundEngine.PostEvent("recoge_botones", gameObject);
        }
    }
}
