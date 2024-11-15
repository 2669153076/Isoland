using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMgr : Singleton<InventoryMgr>
{
    [SerializeField]private List<E_ItemName> itemList = new List<E_ItemName>();
    public List<E_ItemName> ItemList => itemList;

    public ItemDataList_SO itemData;

    private void OnEnable()
    {
        EventHandler.ItemUsedEvent += OnItemUsedEvent;
        EventHandler.ChangeItemEvent += OnChangeItemEvent;
        EventHandler.AfterSceneLoadEvent += OnAfterSceneLoadEvent;
    }

    private void OnDisable()
    {
        EventHandler.ItemUsedEvent -= OnItemUsedEvent;
        EventHandler.ChangeItemEvent -= OnChangeItemEvent;
        EventHandler.AfterSceneLoadEvent -= OnAfterSceneLoadEvent;

    }


    private void OnItemUsedEvent(E_ItemName itemName)
    {
        var index = GetItemIndex(itemName);
        itemList.RemoveAt(index);

        if (itemList.Count <= 0)
            EventHandler.CallUpdateUIEvent(null, -1);
    }


    public void AddItem(E_ItemName itemName)
    {
        if(!itemList.Contains(itemName))
        {
            itemList.Add(itemName);
            //UI显示
            EventHandler.CallUpdateUIEvent(itemData.GetItemDetails(itemName), itemList.Count - 1);
        }
    }

    private int GetItemIndex(E_ItemName itemName)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i] == itemName)
                return i;
        }
        return -1;
    }

    private void OnChangeItemEvent(int index)
    {
        if(index>=0&& index < itemList.Count)
        {
            ItemDetails item = itemData.GetItemDetails(itemList[index]);
            EventHandler.CallUpdateUIEvent(item, index);
        }
    }

    private void OnAfterSceneLoadEvent()
    {
        if (itemList.Count <= 0)
        {
            EventHandler.CallUpdateUIEvent(null, -1);
        }
        else
        {
            for(int i = 0;i < itemList.Count;i++)
            {
                EventHandler.CallUpdateUIEvent(itemData.GetItemDetails(itemList[i]),i);
            }
        }

    }
}
