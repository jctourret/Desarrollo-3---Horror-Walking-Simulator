using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JacksTrap : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] [Range(1,6)] int damageDealt = 1;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.GetComponent<IDamageable>() != null)
        {
            other.transform.GetComponent<IDamageable>().TakeDamage(damageDealt);
        }
    }
}
