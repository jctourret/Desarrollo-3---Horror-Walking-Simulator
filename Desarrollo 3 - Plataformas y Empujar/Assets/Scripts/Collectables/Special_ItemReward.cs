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
                    AkSoundEngine.PostEvent("player_recoje_item", gameObject);
                    other.transform.GetComponentInParent<PlayerStats>().EarnMaxLives(earnScore, earnLive);
                    Destroy(this.gameObject);
                    break;
                
                case RewardType.Other:
                    AkSoundEngine.PostEvent("player_recoje_item", gameObject);
                    Destroy(this.gameObject);
                    break;
            }

        }
    }

}
