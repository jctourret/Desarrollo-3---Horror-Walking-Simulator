using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGizmos_Playtest : MonoBehaviour
{

    public float radius = 2f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(this.transform.position, radius);
    }
}
