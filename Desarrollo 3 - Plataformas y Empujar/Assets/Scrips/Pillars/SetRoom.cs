using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRoom : MonoBehaviour
{
    public Transform parentRoom;

    public List<GameObject> rooms;

    //===============================

    private void OnEnable()
    {
        int random = Random.Range(0, rooms.Count);

        var go = Instantiate(rooms[random], parentRoom);
        go.transform.name = rooms[random].name;
    }

}
