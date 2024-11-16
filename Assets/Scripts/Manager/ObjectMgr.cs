using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMgr : Singleton<ObjectMgr>, ISaveable
{
   private Dictionary<E_ItemName,bool> itemAvailableDic = new Dictionary<E_ItemName, bool>();   //存储道具状态的字典
   private Dictionary<string ,bool> interactiveStateDic = new Dictionary<string ,bool>();   //存储物品状态的字典

    private void Start()
    {
        ISaveable saveable = this;
        saveable.SaveableRegister();
    }

    private void OnEnable()
    {
        EventHandler.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneLoadEvent += OnAfterSceneLoadEvent;
        EventHandler.UpdateUIEvent += OnUpdateUIEvent;
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;
    }


    private void OnDisable()
    {
        EventHandler.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneLoadEvent -= OnAfterSceneLoadEvent;
        EventHandler.UpdateUIEvent -= OnUpdateUIEvent;
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;
    }
    public GameSaveData GenerateSaveData()
    {
        GameSaveData saveData = new GameSaveData();
        saveData.interactiveStateDic = this.interactiveStateDic;
        saveData.itemAvailableDic = this.itemAvailableDic;
        return saveData;
    }

    public void RestoreGameData(GameSaveData saveData)
    {
        this.interactiveStateDic = saveData.interactiveStateDic;
        this.itemAvailableDic = saveData.itemAvailableDic;
    }

    /// <summary>
    /// 卸载场景前的事情
    /// </summary>
    private void OnBeforeSceneUnloadEvent()
    {
        //寻找Item类型的物品，如果字典中没有该物品，则添加并将其设置为显示状态
        foreach (var item in FindObjectsOfType<Item>())
        {
            if (!itemAvailableDic.ContainsKey(item.itemName))
            {
                itemAvailableDic.Add(item.itemName, true);
            }
        }
        //寻找Interactive类型的物品，将其设置为isDone(点击交互是否完成,false表示可以点击,true表示不能点击交互)
        foreach (var item in FindObjectsOfType<Interactive>())
        {
            if(interactiveStateDic.ContainsKey(item.name))
            {
                interactiveStateDic[item.name] = item.isDone;
            }
            else
            {
                interactiveStateDic.Add(item.name, item.isDone);
            }
        }
    }
    /// <summary>
    /// 加载场景后要做的事情
    /// </summary>
    private void OnAfterSceneLoadEvent()
    {
        //寻找场景中Item的物品，如果没有找到该物品，则将其添加到字典中，并将其显示，如果找到了则将其设置为对应状态
        foreach (var item in FindObjectsOfType<Item>())
        {
            if(!itemAvailableDic.ContainsKey(item.itemName))
            {
                itemAvailableDic.Add(item.itemName, true);
            }
            else
            {
                item.gameObject.SetActive(itemAvailableDic[item.itemName]);
            }
        }
        //寻找Interactive类型的物品，将其完成状态设置为对应的状态
        foreach (var item in FindObjectsOfType<Interactive>())
        {
            if (interactiveStateDic.ContainsKey(item.name))
            {
                item.isDone = interactiveStateDic[item.name];
            }
            else
            {
                interactiveStateDic.Add(item.name, item.isDone);
            }
        }
    }

    /// <summary>
    /// 只在拾取物品的时候更新
    /// </summary>
    /// <param name="itemDetails"></param>
    /// <param name="index"></param>
    private void OnUpdateUIEvent(ItemDetails itemDetails, int index)
    {
        if(itemDetails != null)
        {
            itemAvailableDic[itemDetails.itemName] = false;
        }
    }

    private void OnStartNewGameEvent(int gameWeek)
    {
        itemAvailableDic.Clear();
        interactiveStateDic.Clear();
    }
}
