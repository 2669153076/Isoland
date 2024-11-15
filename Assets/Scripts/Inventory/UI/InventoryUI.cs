using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Button leftBtn;
    public Button rightBtn;

    public int curIndex;    //当前所选择的物品序号

    public SoltUI soltUI;

    private void OnEnable()
    {
        leftBtn.onClick.AddListener(() => SwitchItem(-1));
        rightBtn.onClick.AddListener(() => SwitchItem(1));
        EventHandler.UpdateUIEvent += OnUpdateUIEvent;
    }
    private void OnDisable()
    {

        leftBtn.onClick.RemoveAllListeners();
        rightBtn.onClick.RemoveAllListeners();
        EventHandler.UpdateUIEvent -= OnUpdateUIEvent;
    }
    private void OnUpdateUIEvent(ItemDetails details, int index)
    {
        if(details == null)
        {
            soltUI.SetEmpty();
            curIndex = -1;
            leftBtn.interactable = false;
            rightBtn.interactable = false;
        }
        else
        {
            curIndex = index;
            soltUI.SetItem(details);
            if(index>0)
            {
                leftBtn.interactable = true;
            }if(index==-1)
            {
                leftBtn.interactable=false;
                rightBtn.interactable = false;
            }
        }
    }

    /// <summary>
    /// 物品栏左右按钮
    /// </summary>
    /// <param name="amount"></param>
    public void SwitchItem(int amount)
    {
        var index = curIndex + amount;
        if(index<=0)
        {
            leftBtn.interactable = false;
            rightBtn.interactable = true;
        }else if(index>=InventoryMgr.Instance.ItemList.Count-1)
        {
            leftBtn.interactable = true;
            rightBtn.interactable = false;
        }
        else
        {
            leftBtn.interactable = true;
            rightBtn.interactable = true;
        }

        EventHandler.CallChangeItemEvent(index);
    }
}
