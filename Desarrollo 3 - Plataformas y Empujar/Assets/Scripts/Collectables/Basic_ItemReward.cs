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
        MoneyReward
    };

    [SerializeField]
    RewardType rewardType;

    [SerializeField]
    int earnScore = 1;
    
    [SerializeField]
    float jumpForce = 5f;
    
    [SerializeField] [Range(-50,0)]
    float y_fallLimit = -30f;

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            switch (rewardType)
            {
                case RewardType.LifeReward:
                    if (collision.transform.GetComponent<PlayerStats>().EarnLive(earnScore))
                    {
                        Destroy(this.gameObject);
                    }
                    break;

                case RewardType.MoneyReward:
                    EarnMoney(earnScore);
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
