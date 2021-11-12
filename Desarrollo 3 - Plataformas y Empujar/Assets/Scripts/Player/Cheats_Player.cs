using UnityEngine;

public class Cheats_Player : MonoBehaviour
{
    [SerializeField] private PlayerStats player;

    void Update()
    {
        /// Cura Vida:
        if (Input.GetKeyDown(KeyCode.P))
        {
            player.TakeDamage(1);
            Debug.Log("Se daño Magicamente 1!");
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            player.TakeDamage(2);
            Debug.Log("Se daño Magicamente 2!");
        }

        /// Aumenta Dinero:
        if (Input.GetKeyDown(KeyCode.L))
        {
            player.EarnLive(1);
            Debug.Log("Se curo Magicamente 1!");
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            player.EarnLive(2);
            Debug.Log("Se curo Magicamente 2!");
        }

        /// Aumenta Vida Maxima:
        if (Input.GetKeyDown(KeyCode.M))
        {
            player.EarnMaxLives(1, true);
            Debug.Log("Tiene mas Vida Magicamente 1!");
        }

        /// Reduce Vida Maxima:
        if (Input.GetKeyDown(KeyCode.N))
        {
            player.LoseMaxLife(1);
            Debug.Log("Perdiste Vida Maxima Magicamente 2!");
        }

        /// Aumenta Dinero:
        if (Input.GetKeyDown(KeyCode.I))
        {
            player.EarnPlayerMoney(1);
            Debug.Log("Se Sumo Dinero Magicamente!");
        }
    }
}
