using UnityEngine;

public class Special_ItemReward : Collectable
{
    enum RewardType
    {
        MaxLifeReward,
        Other
    };

    [SerializeField] RewardType rewardType;
    [SerializeField] int earnScore = 1;

    [Header("If The Item Grants MaxLife")]
    [SerializeField] bool earnLive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            switch (rewardType)
            {
                case RewardType.MaxLifeReward:
                    other.transform.GetComponentInParent<PlayerStats>().EarnMaxLives(earnScore, earnLive);
                    Destroy(this.gameObject);
                    break;
                
                case RewardType.Other:
                    Destroy(this.gameObject);
                    break;
            }

            AkSoundEngine.PostEvent("player_recoje_item", gameObject);
        }
    }

}
