using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Basic_ItemReward : MonoBehaviour
{
    public static Action<int> EarnMoney;

    enum RewardType
    {
        LifeReward,
        MoneyReward,
        MaxLifeReward
    };

    [SerializeField] RewardType rewardType;

    [SerializeField] int earnScore = 1;
    
    [SerializeField] float jumpForce = 5f;
    
    [SerializeField] [Range(-50,0)]
    float y_fallLimit = -30f;

    [Header("If The Item Grants MaxLife")]
    [SerializeField] bool earnLive = false;

    //===================================

    Rigidbody rig;

    //===================================

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rig.AddForce(Vector3.up * jumpForce, ForceMode.Force);
    }

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

                case RewardType.MaxLifeReward:

                    other.transform.GetComponentInParent<PlayerStats>().EarnMaxLives(earnScore, earnLive);
                    Destroy(this.gameObject);

                    break;
            }
        }
    }

    private void Update()
    {
        DeleteAfterFall();
    }

    //=======================================

    void DeleteAfterFall()
    {
        if (this.transform.position.y < y_fallLimit)
        {
            Destroy(this.gameObject);
        }
    }
}
