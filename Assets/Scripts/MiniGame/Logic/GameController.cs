using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController :Singleton<GameController>
{
    public UnityEvent OnFinish; 
    [Header("游戏数据")]
    public GameH2A_SO gamedata; //数据
    public GameH2A_SO[] gamedataArray;
    public GameObject lineParent;   //线的父物体
    public LineRenderer lineRenderer;   //路径预制体
    public Ball ballPerfab;     //球预制体
        
    public Transform[] holderTransforms;    //球父物体的集合

    private void Start()
    {
        DrawLine();
        CreateBall();
    }

    private void OnEnable()
    {
        EventHandler.CheckBallMatchEvent += OnCheckBallMatchEvent;
    }
    private void OnDisable()
    {
        EventHandler.CheckBallMatchEvent -= OnCheckBallMatchEvent;
    }


    /// <summary>
    /// 绘制路径
    /// </summary>
    public void DrawLine()
    {
       foreach(var connection in gamedata.connectionsList)
        {
            var line = Instantiate(lineRenderer,lineParent.transform);
            line.SetPosition(0, holderTransforms[connection.from].position);
            line.SetPosition(1, holderTransforms[connection.to].position);

            //创建每个Holder的链接关系
            holderTransforms[connection.from].GetComponent<Holder>().linkHolders.Add(holderTransforms[connection.to].GetComponent<Holder>());
            holderTransforms[connection.to].GetComponent<Holder>().linkHolders.Add(holderTransforms[connection.from].GetComponent<Holder>());
        }
    }

    /// <summary>
    /// 绘制球
    /// </summary>
    public void CreateBall()
    {
        for (int i = 0; i < gamedata.startBallOrder.Count; i++)
        {
            if (gamedata.startBallOrder[i] == E_BallName.None)
            {
                holderTransforms[i].GetComponent<Holder>().isEmpty = true;
                continue;
            }
            Ball ball = Instantiate(ballPerfab, holderTransforms[i]);
            holderTransforms[i].GetComponent<Holder>().CheckBall(ball); //检查是否是正确的ball
            holderTransforms[i].GetComponent<Holder>().isEmpty = false;
            ball.SetUpBall(gamedata.GetBallDetails(gamedata.startBallOrder[i]));    //初始化
        }
    }

    /// <summary>
    /// 重新开始mini游戏
    /// </summary>
    public void ResetGame()
    {
        for(int i = 0; i < lineParent.transform.childCount; i++)
        {
            Destroy(lineParent.transform.GetChild(i).gameObject);
        }

        foreach (var holder in holderTransforms)
        {
            if(holder.childCount>0)
            {
                Destroy(holder.GetChild(0).gameObject);
            }
        }
        DrawLine();
        CreateBall();
    }


    /// <summary>
    /// 设置指定周目的游戏数据
    /// </summary>
    /// <param name="week"></param>
    public void SetGameWeekData(int week)
    {
        gamedata = gamedataArray[week];
        //DrawLine();
        //CreateBall();
    }

    private void OnCheckBallMatchEvent()
    {
        foreach (var ball in FindObjectsOfType<Ball>())
        {
            if(!ball.isMatch)
            {
                return;
            }
        }

        foreach(var holder in holderTransforms)
        {
            holder.GetComponent<Collider2D>().enabled = false;
        }
        EventHandler.CallMiniGamePassEvent(gamedata.gameName);
        OnFinish?.Invoke(); //小游戏结束事件
    }

}
