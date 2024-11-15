using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Teleport : MonoBehaviour
{
    [SceneName]public string sceneFrom;
    [SceneName]public string sceneToGo;

    public void TeleportScene()
    {
        TransitionMgr.Instance.Transition(sceneFrom, sceneToGo);
    }
}
