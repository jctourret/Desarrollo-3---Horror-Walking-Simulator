using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRoom : MonoBehaviour
{
    public Transform parentRoom;

    List<GameObject> rooms;

    //===============================

    private void Awake()
    {
        rooms = new List<GameObject>();
        
        Object[] auxRooms;
        auxRooms = Resources.LoadAll("Rooms", typeof(GameObject));
        for (int i = 0; i < auxRooms.Length; i++)
        {
            rooms.Add((GameObject)auxRooms[i]);
        }
    }

    private void OnEnable()
    {
        int random = Random.Range(0, rooms.Count);

        var go = Instantiate(rooms[random], parentRoom);
        go.transform.name = rooms[random].name;
    }

}
