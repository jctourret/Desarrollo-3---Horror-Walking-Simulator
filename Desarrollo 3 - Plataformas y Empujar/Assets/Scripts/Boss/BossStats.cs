using UnityEngine;
using System;

public class BossStats : MonoBehaviour, IDamageable
{
    public static Action<int, int> SetLifeBar;
    public static Action ActivateDeath;

    public GameObject lifeBarPref;

    public int maxLife = 20;
    public int actualLife = 20;

    private bool alive = true;

    //==================================

    private void Start()
    {
        GameObject lifeBar = Instantiate(lifeBarPref);
        lifeBar.transform.SetParent(FindObjectOfType<Canvas>().transform, false);

        actualLife = maxLife;

        SetLifeBar?.Invoke(maxLife, actualLife);

        AkSoundEngine.PostEvent("boss_frase01", gameObject);
    }

    public void TakeDamage(int damage)
    {
        actualLife -= damage;

        SetLifeBar?.Invoke(maxLife, actualLife);

        if (actualLife <= 0)
        {
            Eliminated();
        }
        else
        {
            AkSoundEngine.PostEvent("boss_recibe_dano", gameObject);

            if(actualLife % 10 == 0)
            {
                AkSoundEngine.PostEvent("boss_risas", gameObject);
            }

        }
    }

    public void Eliminated()
    {
        //this.transform.GetComponent<Collider>().enabled = false; //El jefe NO TIENE un boxCollider, tiene un capsuleCollider

        if (!alive)
        {
            Debug.LogError("Eliminacion incorrecta de BOSS: BossStats.cs");
            return;
        }

        ActivateDeath?.Invoke();

        // <-- Acá se llama a la animacion de muerte

        AkSoundEngine.PostEvent("boss_muere", gameObject);

        AkSoundEngine.PostEvent("partida_musica06_victoria", gameObject);

        alive = false;

        LoaderManager.Get().SpawnBasicItem(this.transform.position);
        Destroy(this.transform.parent.gameObject); // Esta linea se tiene que eliminar cuando se tenga la animacion
    }

    //==================================

    public void CallDeathAnimation()    // Esta funcion se va a llamar cuando se haga el final de la animacion de muerte
    {
        Destroy(this.gameObject);
    }
}
