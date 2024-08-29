using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public List<ItemSO> itemsList = new List<ItemSO>();
    public Transform itemContent;
    public GameObject inventoryItem;

    private void Awake()
    {
        instance = this;
    }

    public void Add(ItemSO item)
    {
        itemsList.Add(item);
    }

    public void Remove(ItemSO item)
    {
        itemsList.Remove(item); 
    }

    public void ListItems()
    {
        foreach (Transform item in itemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in itemsList)
        {
            GameObject obj = Instantiate(inventoryItem, itemContent);
            var itemName = obj.transform.Find("Item Name").GetComponent<TMP_Text>();
            var itemIcon = obj.transform.Find("Item Icon").GetComponent<UnityEngine.UI.Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.itemIcon;
        }
    }
}
