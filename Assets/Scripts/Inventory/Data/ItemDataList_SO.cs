using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataList_SO", menuName = "Inventory/ItemDataList_SO")]
public class ItemDataList_SO : ScriptableObject
{
    public List<ItemDetails> itemDetailsList;

    public ItemDetails GetItemDetails(E_ItemName itemName)
    {
        return itemDetailsList.Find(i => i.itemName == itemName);
    }
}

[Serializable]
public class ItemDetails
{

    public E_ItemName itemName;
    public Sprite itemSprite;
}
