using UnityEngine;

public class Boss_AreaAttack : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            Debug.Log("Colisiono con el player");
        }
    }

}
