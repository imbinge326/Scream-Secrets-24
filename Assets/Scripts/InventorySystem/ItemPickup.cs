using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemSO item;

    public void Pickup()
    {
        InventoryManager.instance.Add(item);
        Destroy(gameObject);
    }

    void OnMouseDown()
    {
        Pickup();
    }
}
