using System.Collections.Generic;
using UnityEngine;

public class SpawItemsInMarket : MonoBehaviour
{
    [SerializeField]
    List<ItemAndPoint> storePlaces;

    List<GameObject> itemsInMarket = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < storePlaces.Count; i++)
        {
            var go = Instantiate(storePlaces[i].item, storePlaces[i].marketSlot.position, Quaternion.identity);
            go.GetComponent<BoxCollider>().enabled = false;
            go.GetComponent<Rigidbody>().isKinematic = true;

            go.transform.parent = storePlaces[i].marketSlot;

            itemsInMarket.Add(go);

            storePlaces[i].marketSlot.GetComponent<MarketSlot>().SetItemInMarket(storePlaces[i].item.GetComponent<ItemPrice>().GetCost(), i);
        }
    }

    private void OnEnable()
    {
        MarketSlot.EnableItem += ActivateItem;
    }

    private void OnDisable()
    {
        MarketSlot.EnableItem -= ActivateItem;        
    }

    //=========================================

    void ActivateItem(int id)
    {
        itemsInMarket[id].GetComponent<BoxCollider>().enabled = true;
    }
}

[System.Serializable]
public class ItemAndPoint
{
    [SerializeField]
    public GameObject item;
    [SerializeField]
    public Transform marketSlot;
}