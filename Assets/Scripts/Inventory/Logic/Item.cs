using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public E_ItemName itemName;

    public void ItemClick()
    {
        InventoryMgr.Instance.AddItem(itemName);
        this.gameObject.SetActive(false);

    }
}
