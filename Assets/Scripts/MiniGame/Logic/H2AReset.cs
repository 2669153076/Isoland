using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H2AReset : Interactive
{
    private Transform gearSprite;

    private void Awake()
    {
        gearSprite = transform.GetChild(0);
    }

    public override void EmptyClick()
    {
        //重置游戏
        GameController.Instance.ResetGame();
        gearSprite.DOPunchRotation(Vector3.forward * 180, 1, 1, 0); //沿z轴转180°，转过去再转回来为一次震动，持续1秒，震动次数为1，弹性为0，弹性越小，回复速度越快
    }
}
