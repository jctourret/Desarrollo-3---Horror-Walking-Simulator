using UnityEngine;
using System;
using TMPro;

public class MarketSlot : MonoBehaviour
{
    public static Action<int> PayItem;
    public static Action<int> EnableItem;

    public enum StateObject
    {
        Closed,
        Available,
        Open
    };

    [Header("Interactive Obj")]
    [SerializeField] StateObject stateObject = StateObject.Available;
    [SerializeField] GameObject UIposter;
    [SerializeField] TextMeshPro moneyText;
    
    //========================================

    struct Item
    {
        public int Id;
        public int Cost;
    }

    Item itemMarket;

    bool inRange = false;

    //========================================

    private void Start()
    {
        UIposter.SetActive(false);

        moneyText.text = itemMarket.Cost.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            if (stateObject == StateObject.Available)
            {
                AkSoundEngine.PostEvent("mercado_precio_aparece", gameObject);

                UIposter.SetActive(true);
                inRange = true;
            }
        }
    }

    private void Update()
    {
        if (stateObject == StateObject.Available && inRange == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(PlayerStats.GetPlayerMoney() >= itemMarket.Cost)
                {
                    AkSoundEngine.PostEvent("mercado_compra_item", gameObject);

                    PayItem(itemMarket.Cost);

                    EnableItem(itemMarket.Id);
                    // Acá tengo que llamar al item en especifico y activarle las cosas, solo a ese item.


                    stateObject = StateObject.Open;
                    UIposter.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (stateObject == StateObject.Available)
        {
            UIposter.SetActive(false);

            inRange = false;
        }
    }

    //==========================================

    public void SetItemInMarket(int itemCost, int id)
    {
        itemMarket.Cost = itemCost;
        itemMarket.Id = id;
    }
}
