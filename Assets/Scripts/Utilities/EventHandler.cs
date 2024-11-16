using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventHandler
{
    public static event Action<ItemDetails, int> UpdateUIEvent; //更新UI事件
    public static event Action BeforeSceneUnloadEvent;  //上一个场景卸载前事件
    public static event Action AfterSceneLoadEvent; //之后场景加载后事件
    public static event Action<E_ItemName> ItemUsedEvent;   //物品被使用事件
    public static event Action<int> ChangeItemEvent;    //物品栏切换物品.
    public static event Action<string> ShowDialogueEvent;   //显示对话UI事件
    public static event Action<E_GameState> GameStateChangedEvent;  //游戏状态改变事件
    public static event Action CheckBallMatchEvent;  //检查H2A场景中球是否匹配事件
    public static event Action<string> MiniGamePassEvent;   //游戏通关事件
    public static event Action<int> StartNewGameEvent;  //开始新游戏事件

    public static event Action<ItemDetails, bool> ItemSelectedEvent;    //物体被选中事件

    public static void CallUpdateUIEvent(ItemDetails itemDetails,int i)
    {
        UpdateUIEvent?.Invoke(itemDetails, i);
    }

    public static void CallBeforeSceneUnloadEvent()
    {
        BeforeSceneUnloadEvent?.Invoke();
    }
    public static void CallAfterSceneLoadEvent()
    {
        AfterSceneLoadEvent?.Invoke();
    }
    public static void CallItemSelectedEvent(ItemDetails itemDetails, bool isSel)
    {
        ItemSelectedEvent?.Invoke(itemDetails, isSel);
    }
    public static void CallItemUsedEvent(E_ItemName itemName)
    {
        ItemUsedEvent?.Invoke(itemName);
    }
    public static void CallChangeItemEvent(int index)
    {
        ChangeItemEvent?.Invoke(index);
    }
    public static void CallShowDialogueEvent(string dialogue)
    {
        ShowDialogueEvent?.Invoke(dialogue);
    }
    public static void CallGameStateChangeEvent(E_GameState state)
    {
        GameStateChangedEvent?.Invoke(state);
    }
    public static void CallCheckBallMatchEvent()
    {
        CheckBallMatchEvent?.Invoke();
    }
    public static void CallMiniGamePassEvent(string gameName)
    {
        MiniGamePassEvent?.Invoke(gameName);
    }
    public static void CallStartNewGameEvent(int weekId)
    {
        StartNewGameEvent?.Invoke(weekId);
    }
}
