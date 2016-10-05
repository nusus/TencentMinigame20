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
    /// <summary>
    /// 弹弓的状态
    /// </summary>
    private SlingshotState m_SlingshotState;

    /// <summary>
    /// 限制弹弓的移动区域
    /// </summary>
    public float m_BallMovingClampAngle;
    public float m_BallMovingClampRadius;

    /// <summary>
    /// 弹弓所在的平面和饭团所在的平面的夹角
    /// </summary>
    public float m_PlanesAngle;
    /// <summary>
    /// 弹弓所在的平面和饭团所在的平面的夹角的Sin和Cos值
    /// </summary>
    private float m_CosPlanesAngle;
    private float m_SinPlanesAngle;
    /// <summary>
    /// 移动的灵敏度
    /// </summary>
    public float m_MovingSensitivity;
    /// <summary>
    /// 弹弓的弹力系数
    /// </summary>
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

        m_CosPlanesAngle = Mathf.Cos(Mathf.Deg2Rad * m_PlanesAngle);
        m_SinPlanesAngle = Mathf.Sin(Mathf.Rad2Deg * m_PlanesAngle);

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
            //print("touching position" + Input.touches[0].position);
            //if (m_SlingshotState == SlingshotState.Idle)
            //{
            //    Vector2 touchingPos = new Vector2(Input.touches[0].position.x, Input.touches[0].position.y);
            //    touchingPos = Camera.main.ScreenToWorldPoint(touchingPos);
            //    //print("touching position:" + touchingPos.ToString());

            //    if (IsTouchingBall(touchingPos))
            //    {
            //        print("touching ball");
            //        m_SlingshotState = SlingshotState.Ready;

            //    }
            //}

            //version 1
            if (m_SlingshotState == SlingshotState.Idle)
            {
                Vector2 touchingPos = new Vector2(Input.touches[0].position.x, Input.touches[0].position.y);
                //touchingPos = Camera.main.ScreenToWorldPoint(touchingPos);
                //print("touching position:" + touchingPos.ToString());

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
        //newBallPos += Input.touches[0].deltaPosition / m_FoodSprite.pixelsPerUnit * m_MovingSensitivity;
        Vector3 deltaPosition = Input.touches[0].deltaPosition / m_FoodSprite.pixelsPerUnit * m_MovingSensitivity;
        print(deltaPosition);
        newBallPos.x += deltaPosition.x;
        newBallPos.y += deltaPosition.y;
        if (newBallPos.y > m_BallOrigLocalPosition.y)
        {
            newBallPos.y = m_BallOrigLocalPosition.y;
        }
        newBallPos.z -= deltaPosition.y / m_CosPlanesAngle * m_SinPlanesAngle;

        if (newBallPos.z >= -0.1f)
        {
            newBallPos.z = -0.1f;
        }
        ////clamp ball position
        //Vector2 ballPosVec2 = new Vector2(newBallPos.x, newBallPos.y);
        //Vector2 oriBallPosVce2 = new Vector2(m_BallOrigLocalPosition.x, m_BallOrigLocalPosition.y);
        //Vector2 deltaVec2 = ballPosVec2 - oriBallPosVce2;
        //deltaVec2.y /= m_CosPlanesAngle;
        //if (deltaVec2.sqrMagnitude > m_BallMovingClampRadius)
        //{
        //    deltaVec2.Normalize();
        //    deltaVec2 *= m_BallMovingClampRadius;
        //}
        //float angle = Vector2.Angle(deltaVec2, Vector2.down);
        //if (angle >= m_BallMovingClampAngle) angle = m_BallMovingClampAngle;
        //angle *= Mathf.Deg2Rad;
        //deltaVec2.x = Mathf.Sign(deltaVec2.x) * Mathf.Sin(angle) * deltaVec2.sqrMagnitude;
        //deltaVec2.y = -Mathf.Cos(angle) * deltaVec2.sqrMagnitude;
        //Vector3 ret = new Vector3();
        //ret.x = deltaVec2.x;
        //ret.z = deltaVec2.y * m_SinPlanesAngle;
        //ret.y *= m_CosPlanesAngle;

        //transform.localPosition = m_BallOrigLocalPosition + ret;
        transform.localPosition = newBallPos;
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
        Vector3 leftPos = m_BallOrigLocalPosition - m_LineLeft.transform.localPosition;
        Vector3 rightPos = m_BallOrigLocalPosition - m_LineRight.transform.localPosition;
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
        /*
         * version 1
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

        if (touchingPosition.x >= leftBottom.x
            && touchingPosition.x <= rightTop.x
            && touchingPosition.y >= leftBottom.y
            && touchingPosition.y <= rightTop.y)
        {
            ret = true;
        }
        return ret;
        */

        //version 2
        bool ret = false;
        RaycastHit hit;
        Ray  ray= Camera.main.ScreenPointToRay(touchingPosition);
        if (Physics.Raycast(ray, out hit))
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
