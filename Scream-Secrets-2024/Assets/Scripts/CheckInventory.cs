using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInventory : MonoBehaviour
{
    public ItemSO itemRequired;
    public InventoryManager inventoryManager;
    public GameObject door;

    public void CheckForInventory()
    {
        foreach (var item in inventoryManager.itemsList)
        {
            if (item == itemRequired)
            {
                Destroy(door);
                return;
            }
        }
        Debug.Log("Item not found");
    }
}
