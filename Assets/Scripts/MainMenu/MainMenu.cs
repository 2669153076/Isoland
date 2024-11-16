using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void QuitGame()
    {
        Application.Quit();
    }

    public void ContinueGame()
    {
        SaveLoadMgr.Instance.Load();
    }

    public void GoBackToMenu()
    {
        var curScene = SceneManager.GetActiveScene().name;
        TransitionMgr.Instance.Transition(curScene, "MainMenu");
        SaveLoadMgr.Instance.Save();
    }

    public void StartGameWeek(int gameWeek)
    {
        EventHandler.CallStartNewGameEvent(gameWeek);
    }
}
