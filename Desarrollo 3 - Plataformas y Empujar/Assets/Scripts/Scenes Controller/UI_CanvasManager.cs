using System.Collections.Generic;
using UnityEngine;

public class UI_CanvasManager : MonoBehaviour
{
    public GameObject deathBox;

    public List<GameObject> playerUI = new List<GameObject>();

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
        deathBox.GetComponent<Animator>().SetTrigger("Death");

        foreach (GameObject item in playerUI)
        {
            item.SetActive(false);
        }
    }

}
