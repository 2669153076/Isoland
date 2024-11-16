using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveData
{
    public int gameWeek;
    public string curScene;

    public Dictionary<string,bool> miniGameStateDic;

    public Dictionary<E_ItemName, bool> itemAvailableDic ;   //存储道具状态的字典
    public Dictionary<string, bool> interactiveStateDic ;   //存储物品状态的字典

    public List<E_ItemName> itemList;
}