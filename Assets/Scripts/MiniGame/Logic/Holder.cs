using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : Interactive
{
    public E_BallName matchBall;    //格子中应该是正确的球
    private Ball currentBall;   //当前格子中的球

    public HashSet<Holder> linkHolders = new HashSet<Holder>();
    public bool isEmpty;

    public void CheckBall(Ball ball)
    {
        currentBall = ball;
        if(ball.ballDetails.ballName == matchBall)
        {
            currentBall.isMatch = true;
            currentBall.SetRight();
        }
        else
        {
            currentBall.isMatch = false;
            currentBall.SetWrong();
        }
    }

    public override void EmptyClick()
    {
       foreach(Holder holder in linkHolders)
        {
            if(holder.isEmpty)
            {
                currentBall.transform.position = holder.transform.position;
                currentBall.transform.SetParent(holder.transform);

                holder.CheckBall(currentBall);
                this.currentBall = null;

                this.isEmpty = true;
                holder.isEmpty = false;

                EventHandler.CallCheckBallMatchEvent();
            }
        }
    }


}
