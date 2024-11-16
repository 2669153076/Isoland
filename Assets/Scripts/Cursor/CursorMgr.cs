using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorMgr : Singleton<CursorMgr>
{
    public RectTransform hand;

    private Vector3 mouseWorldPos => Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
    private bool canClick;

    private E_ItemName curItem; //当前选中物品
    private bool holdItem;  //是否显示手

    private void OnEnable()
    {
        EventHandler.ItemSelectedEvent += OnItemSelectedEvent;
        EventHandler.ItemUsedEvent += OnItemUsedEvent;
    }

    private void OnDisable()
    {
        EventHandler.ItemSelectedEvent -= OnItemSelectedEvent;
        EventHandler.ItemUsedEvent -= OnItemUsedEvent;
    }

    private void Update()
    {
        canClick = ObjectAtMousePosition();

        if (hand.gameObject.activeInHierarchy)
        {
            hand.position = Input.mousePosition;
        }

        if(InteraciWithUI())
        {
            return;
        }

        if (canClick && Input.GetMouseButtonDown(0))
        {
            ClickAction(ObjectAtMousePosition().gameObject);
        }
    }

    /// <summary>
    /// 点击事件
    /// </summary>
    /// <param name="clickObj"></param>
    private void ClickAction(GameObject clickObj)
    {
        switch (clickObj.tag)
        {
            case "Teleport":
                var teleport = clickObj.GetComponent<Teleport>();
                teleport?.TeleportScene();
                break;
            case "Item":
                var item = clickObj.GetComponent<Item>();
                item?.ItemClick();
                break;
            case "Interactive":
                var interactive = clickObj.GetComponent<Interactive>();
                if (holdItem)
                    interactive?.CheckItem(curItem);
                else
                    interactive?.EmptyClick();
                break;
        }
    }


    /// <summary>
    /// 检测鼠标点击范围内的碰撞体
    /// </summary>
    /// <returns></returns>
    private Collider2D ObjectAtMousePosition()
    {
        return Physics2D.OverlapPoint(mouseWorldPos);
    }


    private void OnItemSelectedEvent(ItemDetails itemDetails, bool isSel)
    {

        holdItem = isSel;
        if (isSel)
        {
            curItem = itemDetails.itemName;

        }
        hand.gameObject.SetActive(holdItem);
    }

    private void OnItemUsedEvent(E_ItemName name)
    {
        curItem = E_ItemName.None;
        holdItem = false;
        hand.gameObject.SetActive(holdItem);
    }

    /// <summary>
    /// 判断鼠标是否与UI互动
    /// </summary>
    /// <returns></returns>
    private bool InteraciWithUI()
    {
        if(EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }
        return false;
    }
}
