using UnityEngine;

public class ItemPrice : MonoBehaviour
{
    [SerializeField] int cost = 2;

    public int GetCost()
    {
        return cost;
    }
}
