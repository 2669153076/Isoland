using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SoltUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image itemImage;
    private ItemDetails curItem;
    private bool isSelected;

    public ItemToolTip itemToolTip;


    /// <summary>
    /// 设置当前所点击道具
    /// </summary>
    /// <param name="itemDetails"></param>
    public void SetItem(ItemDetails itemDetails)
    {
        curItem = itemDetails;
        this.gameObject.SetActive(true);
        itemImage.sprite = itemDetails.itemSprite;
        itemImage.SetNativeSize();
    }

    public void SetEmpty()
    {
        this.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.gameObject.activeInHierarchy && curItem != null)
        {
            itemToolTip.gameObject.SetActive(true);
            itemToolTip.UpdateItemName(curItem.itemName);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (this.gameObject.activeInHierarchy)
        {
            itemToolTip.gameObject.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isSelected = !isSelected;
        if (curItem != null)
        {
            EventHandler.CallItemSelectedEvent(curItem, isSelected);
        }
    }
}
