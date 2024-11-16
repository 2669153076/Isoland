using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameH2A_SO", menuName = "Mini Game Data/GameH2A_SO")]
public class GameH2A_SO : ScriptableObject
{
    [SceneName]public string gameName;
    [Header("球的名字和对应的图片")]
    public List<BallDetails> ballDetailsList;

    [Header("游戏数据逻辑")]
    public List<Connections> connectionsList;      //连线数据列表
    public List<E_BallName> startBallOrder; //开始游戏的球的顺序

    public BallDetails GetBallDetails(E_BallName ballName)
    {
        return ballDetailsList.Find(b => b.ballName == ballName);
    }
}

[System.Serializable]
public class BallDetails
{
    public E_BallName ballName;
    public Sprite wrongSprite;
    public Sprite rightSprite;
}

[System.Serializable]
public class Connections {
    public int from;
    public int to;

}
