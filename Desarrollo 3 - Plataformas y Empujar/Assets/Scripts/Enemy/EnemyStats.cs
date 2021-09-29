using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour, IDamageable
{
    public int life = 10;

    //==================================
    [HideInInspector]
    public bool isLive = true;
    //==================================

    public void TakeDamage(int damage)
    {
        life -= damage;

        if (life <= 0)
            Eliminated();
    }

    public void Eliminated()
    {
        isLive = false;

        this.transform.GetComponent<BoxCollider>().enabled = false;

        // <-- Acá se llama a la animacion de muerte

        LoaderManager.Get().SpawnBasicItem(this.transform.position, EnemyManager.instance.cam);

        Destroy(this.transform.parent.gameObject); // Esta linea se tiene que eliminar cuando se tenga la animacion
    }

    //==================================

    public void CallDeathAnimation()    // Esta funcion se va a llamar cuando se haga el final de la animacion de muerte
    {
        Destroy(this.gameObject);
    }
}
