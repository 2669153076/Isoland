using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMgr : MonoBehaviour, ISaveable
{
    private Dictionary<string ,bool> miniGameStateDic = new Dictionary<string ,bool>();

    private int gameWeek;   //游戏周目
    private GameController curGame; //当前游戏控制器

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
        EventHandler.CallGameStateChangeEvent(E_GameState.GamePlay);

        //保存数据
        ISaveable saveable = this;
        saveable.SaveableRegister();
    }

    private void OnEnable()
    {
        EventHandler.AfterSceneLoadEvent += OnAfterSceneLoadEvent;
        EventHandler.MiniGamePassEvent += OnMiniGamePassEvent;
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;
    }


    private void OnDisable()
    {
        EventHandler.AfterSceneLoadEvent -= OnAfterSceneLoadEvent;
        EventHandler.MiniGamePassEvent -= OnMiniGamePassEvent;
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;
    }


    public GameSaveData GenerateSaveData()
    {
        GameSaveData saveData = new GameSaveData();
        saveData.gameWeek = this.gameWeek;
        saveData.miniGameStateDic = this.miniGameStateDic;
        return saveData;
    }

    public void RestoreGameData(GameSaveData saveData)
    {
        this.gameWeek = saveData.gameWeek;
        this.miniGameStateDic = saveData.miniGameStateDic;
    }


    private void OnAfterSceneLoadEvent()
    {
        //修改minigame通关状态
        foreach (var miniGame in FindObjectsOfType<MiniGame>())
        {
            if(miniGameStateDic.TryGetValue(miniGame.gameName,out bool isPass))
            {
                miniGame.isPass = isPass;
                miniGame.UpdateMiniGameState();
            }
        }

        curGame = FindObjectOfType<GameController>();
        curGame?.SetGameWeekData(gameWeek);
    }

    private void OnMiniGamePassEvent(string gameName)
    {
        miniGameStateDic[gameName] = true;
    }

    private void OnStartNewGameEvent(int gameWeek)
    {
        this.gameWeek = gameWeek;
        miniGameStateDic.Clear();
    }

}
