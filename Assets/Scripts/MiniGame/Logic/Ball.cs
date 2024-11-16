using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public BallDetails ballDetails;

    public bool isMatch;    //两者是否匹配

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetUpBall(BallDetails ballDetails)
    {
        this.ballDetails = ballDetails;

        if (isMatch)
        {
            SetRight();
        }
        else
        {
            SetWrong();
        }
    }

    public void SetRight()
    {
        this.spriteRenderer.sprite =  ballDetails.rightSprite;
    }

    public void SetWrong()
    {
        this.spriteRenderer.sprite = ballDetails.wrongSprite;
    }
}
