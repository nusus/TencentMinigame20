using UnityEngine;
using System.Collections;

public class BallController2D : MonoBehaviour {

    protected enum SlingshotState { Idle, Ready, Shotting };

    //小球原来的位置  
    private Vector3 m_BallOrigLocalPosition;

    //左侧LineRenderer  
    public LineRenderer m_LineLeft;
    //右侧LineRenderer  
    public LineRenderer m_LineRight;

    private Sprite m_FoodSprite;
    /// <summary>
    /// 弹弓的状态
    /// </summary>
    private SlingshotState m_SlingshotState;
    /// <summary>
    /// 移动的灵敏度
    /// </summary>
    public float m_MovingSensitivity;
    /// <summary>
    /// 弹弓的弹力系数
    /// </summary>
    public float m_SlingFactor;
    public float m_BallMovingClmapRadius;
    void Start()
    {
        m_FoodSprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;

        m_BallOrigLocalPosition = transform.localPosition;

        m_SlingshotState = SlingshotState.Idle;

        ChangeRopePosition();

        if (m_MovingSensitivity <= 0.0f)
        {
            m_MovingSensitivity = 1.0f;
        }
    }

    void Update()
    {
        if (Input.touchCount <= 0 || Input.touchCount > 1)
        {
            m_SlingshotState = SlingshotState.Idle;
            return;
        }

        if (Input.touches[0].phase == TouchPhase.Began)
        {
            //version 1
            if (m_SlingshotState == SlingshotState.Idle)
            {
                Vector2 touchingPos = new Vector2(Input.touches[0].position.x, Input.touches[0].position.y);

                if (IsTouchingBall(touchingPos))
                {
                    print("touching ball");
                    m_SlingshotState = SlingshotState.Ready;

                }
            }
        }

        if (Input.touches[0].phase == TouchPhase.Moved)
        {
            if (m_SlingshotState == SlingshotState.Ready)
            {
                ChangeBallPosition();
                ChangeRopePosition();
            }
            else
            {
                m_SlingshotState = SlingshotState.Idle;
            }
        }

        if (Input.touches[0].phase == TouchPhase.Ended)
        {
            if (m_SlingshotState == SlingshotState.Ready)
            {
                ShotBall();
                RopeRevert();
            }
        }
    }

    protected void ChangeBallPosition()
    {
        Vector3 newBallPos = transform.localPosition;
        Vector2 tmpDeltaPosition = Input.touches[0].deltaPosition / m_FoodSprite.pixelsPerUnit * m_MovingSensitivity;
        newBallPos.x += tmpDeltaPosition.x;
        newBallPos.y += tmpDeltaPosition.y;
        Vector3 deltaPos = newBallPos - m_BallOrigLocalPosition;
        if(deltaPos.x * deltaPos.x + deltaPos.y * deltaPos.y > m_BallMovingClmapRadius * m_BallMovingClmapRadius)
        {
            //print("deltaPositionMagnitude:" + deltaPos.sqrMagnitude.ToString());
            deltaPos.Normalize();
            deltaPos *= m_BallMovingClmapRadius;
            //print("deltaPositionMagnitude:" + deltaPos.sqrMagnitude.ToString());
        }
        transform.localPosition = m_BallOrigLocalPosition + deltaPos;
    }

    protected void ChangeRopePosition()
    {
        Vector3 leftPos = transform.localPosition - m_LineLeft.transform.localPosition;
        Vector3 rightPos = transform.localPosition - m_LineRight.transform.localPosition;
        ChangeRopePosition(leftPos, rightPos);
    }

    protected void ShotBall()
    {
        Vector3 dir = (m_BallOrigLocalPosition - transform.localPosition).normalized;
        transform.GetComponent<Rigidbody2D>().gravityScale = 1;
        transform.GetComponent<Rigidbody2D>().AddForce(dir * m_SlingFactor, ForceMode2D.Impulse);
        m_SlingshotState = SlingshotState.Shotting;
    }

    protected void RopeRevert()
    {
        Vector3 leftPos = m_BallOrigLocalPosition - m_LineLeft.transform.localPosition;
        Vector3 rightPos = m_BallOrigLocalPosition - m_LineRight.transform.localPosition;
        ChangeRopePosition(leftPos, rightPos);
        m_SlingshotState = SlingshotState.Idle;
    }

    protected void BallRevert()
    {
        transform.localPosition = m_BallOrigLocalPosition;
        transform.GetComponent<Rigidbody2D>().gravityScale = 0;
        transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    private void ChangeRopePosition(Vector3 leftPos, Vector3 rightPos)
    {
        m_LineLeft.SetPosition(1, leftPos);
        m_LineRight.SetPosition(1, rightPos);
    }


    protected bool IsTouchingBall(Vector2 touchingPosition)
    {
        //version 2
        bool ret = false;
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touchingPosition), Vector2.zero);
        if (hit.collider != null)
        {
            if (hit.transform.name.Equals("food"))
            {
                ret = true;
            }
        }
        return ret;
    }

    public void ReShootBall()
    {
        BallRevert();
        RopeRevert();
    }
}
