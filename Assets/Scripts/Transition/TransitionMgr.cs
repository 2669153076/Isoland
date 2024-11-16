using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionMgr : Singleton<TransitionMgr>, ISaveable
{
    [SceneName] public string startScene;

    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration;  //转换速度
    private bool isFade;    //是否处于缓入缓出

    private bool canTransition; //是否可以跳转场景

    private void Start()
    {
        //StartCoroutine(TransitionToScene(string.Empty, startScene));
        ISaveable saveable = this;
        saveable.SaveableRegister();
    }

    private void OnEnable()
    {
        EventHandler.GameStateChangedEvent += OnGameStateChangeEvent;
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;
    }

    private void OnDisable()
    {
        EventHandler.GameStateChangedEvent -= OnGameStateChangeEvent;
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;
    }

    public GameSaveData GenerateSaveData()
    {
        GameSaveData saveData = new GameSaveData();
        saveData.curScene = SceneManager.GetActiveScene().name;
        return saveData;
    }

    public void RestoreGameData(GameSaveData saveData)
    {
        Transition("MainMenu", saveData.curScene);
    }

    public void Transition(string from,string to)
    {
        if (!isFade&&canTransition)
            StartCoroutine(TransitionToScene(from, to));
    }

    private IEnumerator TransitionToScene(string from,string to)
    {
        yield return Fade(1);

        if (from != string.Empty)
        {
            EventHandler.CallBeforeSceneUnloadEvent();
            yield return SceneManager.UnloadSceneAsync(from);
        }

        yield return SceneManager.LoadSceneAsync(to,LoadSceneMode.Additive);

        //设置新场景为激活场景
        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newScene);

        EventHandler.CallAfterSceneLoadEvent();
        yield return Fade(0);

    }

    /// <summary>
    /// 淡入淡出场景
    /// </summary>
    /// <param name="targetAlpha">1是黑 0是透明</param>
    /// <returns></returns>
    private IEnumerator Fade(float targetAlpha)
    {
        isFade = true;

        fadeCanvasGroup.blocksRaycasts = true;

        float speed = Mathf.Abs(fadeCanvasGroup.alpha-targetAlpha)/fadeDuration;

        while(!Mathf.Approximately(fadeCanvasGroup.alpha,targetAlpha))  //fadeCanvasGroup的alpha值是否宇target的alpha值相似
        {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha,targetAlpha,speed*Time.deltaTime);  //逐步平滑过渡
            yield return null;
        }

        fadeCanvasGroup.blocksRaycasts=false;
        isFade = false;
    }

    private void OnGameStateChangeEvent(E_GameState state)
    {
        canTransition = state == E_GameState.GamePlay;
    }

    private void OnStartNewGameEvent(int gameWeek)
    {
        StartCoroutine(TransitionToScene("MainMenu", startScene));
    }
}
