﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderManager : MonoBehaviourSingleton<LoaderManager>
{
    public override void Awake()
    {
        base.Awake();

        LoadBasicItems();
        LoadRooms();
    }

    //=====================================================================================
    //  Abilities Loader



    //=====================================================================================
    //  Item Spawner

    // Porcentage of appear Basic Items:
    const int MAX_PORCENTAGE = 100;
    const int nothing = 30;
    const int something = 100;

    private List<GameObject> basicItems;

    private void LoadBasicItems()
    {
        basicItems = new List<GameObject>();

        Object[] allBItems;
        allBItems = Resources.LoadAll("Collectable_Items/BasicItems", typeof(GameObject));
        foreach (GameObject item in allBItems)
        {
            basicItems.Add((GameObject)item);
        }

        GameObject BItemsParent = new GameObject();
        BItemsParent.transform.name = "// --- Items --- \\\\";
    }

    public void SpawnBasicItem(Vector3 position)
    {
        int random = Random.Range(0, MAX_PORCENTAGE);

        if(random > nothing)
        {
            random = Random.Range(0, basicItems.Count);

            var go = Instantiate(basicItems[random], position, Quaternion.Euler(Vector3.up));
            go.transform.name = basicItems[random].name;
        }
    }

    //=====================================================================================
    //  Rooms Spawner

    private List<GameObject> rooms;

    private void LoadRooms()
    {
        rooms = new List<GameObject>();

        Object[] auxRooms;
        auxRooms = Resources.LoadAll("Rooms", typeof(GameObject));
        foreach (GameObject room in auxRooms)
        {
            rooms.Add((GameObject)room);
        }
    }

    public GameObject GetARoom()
    {
        int random = Random.Range(0, rooms.Count);

        return rooms[random];
    }

    public List<GameObject> GetRooms()
    {
        return rooms;
    }
}