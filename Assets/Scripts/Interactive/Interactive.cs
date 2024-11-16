using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 点击函数
/// tag Interactive表示可点击物体
/// </summary>
public class Interactive : MonoBehaviour
{
    public E_ItemName requireItem;

    public bool isDone; //互动是否结束

    public void CheckItem(E_ItemName itemName)
    {
        if(itemName == requireItem&&!isDone)
        {
            isDone = true;  //互动结束，不需要互动

            OnClickAction();
            EventHandler.CallItemUsedEvent(itemName);
        }
    }

    /// <summary>
    /// 正确的物品被点击后执行的方法
    /// </summary>
    protected virtual void OnClickAction() { }

    /// <summary>
    /// 空点击
    /// </summary>
    public virtual void EmptyClick() { }
}
