using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRoom : MonoBehaviour
{
    public List<GameObject> affectedRoom;

    private void OnTriggerEnter(Collider other)
    {

        foreach (GameObject room in affectedRoom)
        {
            room.GetComponent<IRoom>().StartBehaviour();
        }

        this.gameObject.SetActive(false);

    }
}
