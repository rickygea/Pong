using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    public ballcontrol ball;
    public CircleCollider2D ballCollider;
    public Rigidbody2D ballRigidbody;
    public GameObject ballAtCollision;
     bool drawBallAtCollision = false;

    private void Update()
    {

        Vector2 offsetHitPoint = new Vector2();
        RaycastHit2D[] circleCastHit2DArray = Physics2D.CircleCastAll(ballRigidbody.position, ballCollider.radius,ballRigidbody.velocity.normalized);

        foreach (RaycastHit2D circleCastHit2D in circleCastHit2DArray)
        {
            if (circleCastHit2D.collider != null && circleCastHit2D.collider.GetComponent<ballcontrol>() == null)
            {
                Vector2 hitPoint = circleCastHit2D.point;
                Vector2 hitNormal = circleCastHit2D.normal;
                offsetHitPoint = hitPoint + hitNormal * ballCollider.radius;
                DottedLine.DottedLine.Instance.DrawDottedLine(ball.transform.position, offsetHitPoint);

                if (circleCastHit2D.collider.GetComponent<sidewall>() == null)
                {
                    Vector2 inVector = (offsetHitPoint - ball.TrajectoryOrigin).normalized;
                    
                    Vector2 outVector = Vector2.Reflect(inVector, hitNormal);
                    
                    float outDot = Vector2.Dot(outVector, hitNormal);
                    if (outDot > -1.0f && outDot < 1.0)
                    {
                        DottedLine.DottedLine.Instance.DrawDottedLine(offsetHitPoint,offsetHitPoint + outVector * 10.0f);
                        
                        drawBallAtCollision = true;
                    }
                }
                
                break;
            }


        }
        if (drawBallAtCollision)
        {
            ballAtCollision.transform.position = offsetHitPoint;
            ballAtCollision.SetActive(true);
        }
        else
        {
            ballAtCollision.SetActive(false);
        }
    }
}
