﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderManager : MB_SingletonDestroy<LoaderManager>
{
    GameObject ItemsParent = null;

    public override void Awake()
    {
        base.Awake();

        LoadBasicItems();
        LoadSpecialItems();
        LoadRooms();

        ItemsParent = new GameObject();
        ItemsParent.transform.name = "// --- Items --- \\\\";
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

    private List<GameObject> specialItems;

    private void LoadBasicItems()
    {
        basicItems = new List<GameObject>();

        Object[] allBItems;
        allBItems = Resources.LoadAll("Collectable_Items/BasicItems", typeof(GameObject));
        foreach (GameObject item in allBItems)
        {
            basicItems.Add((GameObject)item);
        }
    }

    private void LoadSpecialItems()
    {
        specialItems = new List<GameObject>();

        Object[] allSItems;
        allSItems = Resources.LoadAll("Collectable_Items/SpecialItems", typeof(GameObject));
        foreach (GameObject item in allSItems)
        {
            specialItems.Add((GameObject)item);
        }
    }

    public void SpawnBasicItem(Vector3 position)
    {
        int random = Random.Range(0, MAX_PORCENTAGE);

        if(random > nothing)
        {
            random = Random.Range(0, basicItems.Count);

            var item = Instantiate(basicItems[random], position, Quaternion.Euler(Vector3.up));
            item.transform.name = basicItems[random].name;

            item.transform.parent = ItemsParent.transform;
        }
    }

    public void SpawnSpecialItem(Vector3 position, Camera cam)
    {
        int random = Random.Range(0, MAX_PORCENTAGE);

        if (random > nothing)   // Si tiene suerte sale algo bueno
        {
            random = Random.Range(0, specialItems.Count);

            var item = Instantiate(specialItems[random], position, Quaternion.Euler(Vector3.up));
            item.transform.name = specialItems[random].name;

            item.transform.parent = ItemsParent.transform;

            item.GetComponentInChildren<LookToCamera>().cam = cam;
        }
        else                    // Sino le sale un item comun
        {
            random = Random.Range(0, basicItems.Count);

            var item = Instantiate(basicItems[random], position, Quaternion.Euler(Vector3.up));
            item.transform.name = basicItems[random].name;

            item.transform.parent = ItemsParent.transform;

            item.GetComponentInChildren<LookToCamera>().cam = cam;
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
