using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour
{
    protected enum SlingshotState { Idle, Ready, Shotting  };

    //小球原来的位置  
    private Vector3 m_BallOrigLocalPosition;

    //左侧LineRenderer  
    private LineRenderer m_LineLeft;
    //右侧LineRenderer  
    private LineRenderer m_LineRight;

    private Sprite m_FoodSprite;

    private SlingshotState m_SlingshotState;

    public Vector2 m_BallMovingAreaLeftBottom;
    public Vector2 m_BallMovingAreaRightTop;

    public float m_MovingSensitivity;
    public float m_SlingFactor;

    void Start()
    {
        //获取LineRenderer  
        m_LineLeft = GameObject.Find("slingshot").transform.FindChild("ropeLeft").
            transform.GetComponent<LineRenderer>();
        m_LineRight = GameObject.Find("slingshot").transform.FindChild("ropeRight").
            transform.GetComponent<LineRenderer>();

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
        if (Input.touchCount <= 0 || Input.touchCount >1)
        {
            m_SlingshotState = SlingshotState.Idle;
            return;
        }

        if (Input.touches[0].phase == TouchPhase.Began)
        {
            if (m_SlingshotState == SlingshotState.Idle)
            {
                Vector2 touchingPos = new Vector2(Input.touches[0].position.x, Input.touches[0].position.y);
                touchingPos = Camera.main.ScreenToWorldPoint(touchingPos);
                print("touching position:" + touchingPos.ToString());
                if (IsTouchingBall(touchingPos))
                {
                    print("touching ball");
                    m_SlingshotState = SlingshotState.Ready;

                }
            }
        }

        if (Input.touches[0].phase == TouchPhase.Moved)
        {
            //print("touching position moved" + Input.touches[0].deltaPosition.ToString());
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

        if (Input.GetMouseButtonUp(0))
        {
            if (m_SlingshotState == SlingshotState.Ready)
            {
                ShotBall();
                //RopeRevert();
            }
        }
    }

    protected void ChangeBallPosition()
    {
        Vector2 newBallPos = new Vector2(transform.localPosition.x, transform.localPosition.y);
        newBallPos += Input.touches[0].deltaPosition / m_FoodSprite.pixelsPerUnit * m_MovingSensitivity;
        transform.localPosition = ClampTouchingPosition(newBallPos);
    }

    protected void ChangeRopePosition()
    {
        Vector3 leftPos = transform.localPosition - m_LineLeft.transform.localPosition;
        Vector3 rightPos = transform.localPosition - m_LineRight.transform.localPosition;
        ChangeRopePosition(leftPos, rightPos);
    }

    protected void ShotBall()
    {
        //TODO:
        Vector3 dir = (m_BallOrigLocalPosition - transform.localPosition).normalized;
        transform.GetComponent<Rigidbody>().useGravity = true;
        transform.GetComponent<Rigidbody>().AddForce(dir * m_SlingFactor, ForceMode.Impulse);
        m_SlingshotState = SlingshotState.Shotting;
    }

    protected void RopeRevert()
    {
        Vector3 leftPos = transform.localPosition - m_LineLeft.transform.localPosition;
        Vector3 rightPos = transform.localPosition - m_LineRight.transform.localPosition;
        ChangeRopePosition(leftPos, rightPos);
        m_SlingshotState = SlingshotState.Idle;
    }

    protected void BallRevert()
    {
        transform.localPosition = m_BallOrigLocalPosition;
        transform.GetComponent<Rigidbody>().useGravity = false;
        transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void ChangeRopePosition(Vector3 leftPos, Vector3 rightPos)
    {
        m_LineLeft.SetPosition(1, leftPos);
        m_LineRight.SetPosition(1, rightPos);
    }
  

    protected bool IsTouchingBall(Vector2 touchingPosition)
    {
        bool ret = false;
        float pixelsPerUnit = m_FoodSprite.pixelsPerUnit;
        Vector2 leftBottom = new Vector2(
            transform.position.x - m_FoodSprite.rect.width * transform.lossyScale.x / 2 / pixelsPerUnit,
            transform.position.y - m_FoodSprite.rect.height * transform.lossyScale.y / 2 / pixelsPerUnit
            );
        Vector2 rightTop = new Vector2(
            transform.position.x + m_FoodSprite.rect.width * transform.lossyScale.x / 2 / pixelsPerUnit,
            transform.position.y + m_FoodSprite.rect.height * transform.lossyScale.y / 2 / pixelsPerUnit
            );

        if(touchingPosition.x >= leftBottom.x 
            && touchingPosition.x <= rightTop.x
            && touchingPosition.y >= leftBottom.y
            && touchingPosition.y <= rightTop.y)
        {
            ret = true;
        }
        return ret;
    }

    Vector3 ClampTouchingPosition(Vector2  touchingPosition)
    {
        Vector3 ret = new Vector3(touchingPosition.x, touchingPosition.y, 0);
        ret.x = Mathf.Clamp(touchingPosition.x, m_BallMovingAreaLeftBottom.x, m_BallMovingAreaRightTop.x);
        ret.y = Mathf.Clamp(touchingPosition.y, m_BallMovingAreaLeftBottom.y, m_BallMovingAreaRightTop.y);
        ret.z = m_BallOrigLocalPosition.z - Mathf.Sqrt( Mathf.Pow(ret.x - m_BallOrigLocalPosition.x, 2) + Mathf.Pow(ret.y - m_BallOrigLocalPosition.y, 2) ) / 2;
        return ret;
    }

    public void ReShootBall()
    {
        BallRevert();
        RopeRevert();
    }
}
