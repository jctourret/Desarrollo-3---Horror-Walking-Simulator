using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour, IDamageable
{
    public int life = 10;

    bool alive = true;

    //==================================

    public void TakeDamage(int damage)
    {
        life -= damage;

        if (life <= 0)
            Eliminated();
    }

    public void Eliminated()
    {
        //this.transform.GetComponent<Collider>().enabled = false; //El jefe NO TIENE un boxCollider, tiene un capsuleCollider

        // <-- Acá se llama a la animacion de muerte


        if (!alive)
        {
            Debug.LogError("Algo pasa");
            return;
        }

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
