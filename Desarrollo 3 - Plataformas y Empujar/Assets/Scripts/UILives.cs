using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UILives : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerStats.OnPlayerDamaged += removeLife;
    }

    private void OnDisable()
    {
        PlayerStats.OnPlayerDamaged -= removeLife;
    }

    void removeLife()
    {
        for(int i = transform.childCount-1; i != -1; i--)
        {
            if (transform.GetChild(i).gameObject.activeInHierarchy)
            {
                transform.GetChild(i).gameObject.SetActive(false);
                return;
            }
        }
    }

}
