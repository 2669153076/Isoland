using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class SaveLoadMgr : Singleton<SaveLoadMgr>
{
    private string jsonFolder;
    private List<ISaveable> saveableList = new List<ISaveable>();
    private Dictionary<string , GameSaveData> saveDataDic = new Dictionary<string , GameSaveData>();

    protected override void Awake()
    {
        base.Awake();
        jsonFolder = Application.persistentDataPath + "/SAVE/";
    }

    private void OnEnable()
    {
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;
    }
    private void OnDisable()
    {
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;
    }

    /// <summary>
    /// 注册数据
    /// </summary>
    /// <param name="saveable"></param>
    public void Register(ISaveable saveable)
    {
        saveableList.Add(saveable);
    }
    /// <summary>
    /// 保存数据
    /// </summary>
    public void Save()
    {
        saveableList.Clear();

        foreach (ISaveable saveable in saveableList)
        {
            saveDataDic.Add(saveable.GetType().Name, saveable.GenerateSaveData());
        }

        var resultPath = jsonFolder + "/data.sav";

        var jsonData = JsonConvert.SerializeObject(saveDataDic, Formatting.Indented);

        if(!File.Exists(resultPath))
        {
            Directory.CreateDirectory(resultPath);
        }

        File.WriteAllText(resultPath, jsonData);
    }
    /// <summary>
    /// 读取数据
    /// </summary>
    public void Load()
    {
        var resultPath = jsonFolder + "/data.sav";

        if(!File.Exists(resultPath))
        {
            return;
        }

        var stringData = File.ReadAllText(resultPath);
        var jsonData = JsonConvert.DeserializeObject<Dictionary<string,GameSaveData>>(stringData);

        foreach(var saveable in saveableList)
        {
            saveable.RestoreGameData(jsonData[saveable.GetType().Name]);
        }
    }


    private void OnStartNewGameEvent(int gameWeek)
    {
        var resultPath = jsonFolder + "data.sav";
        if(File.Exists(resultPath))
        {
            File.Delete(resultPath);
        }
    }

}
