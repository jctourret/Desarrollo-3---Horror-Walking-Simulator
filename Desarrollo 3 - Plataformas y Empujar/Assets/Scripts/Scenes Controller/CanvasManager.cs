using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject deathBox;

    private void OnEnable()
    {
        PlayerMovement.OnPlayerFallDeath += enableDeathBox;
        PlayerStats.OnPlayerDamageDeath += enableDeathBox;
    }

    private void OnDisable()
    {
        PlayerMovement.OnPlayerFallDeath -= enableDeathBox;
        PlayerStats.OnPlayerDamageDeath -= enableDeathBox;
    }

    void enableDeathBox()
    {
        deathBox.SetActive(true);
    }

}
