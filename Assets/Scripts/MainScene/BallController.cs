using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour
{
    protected enum SlingshotState { Idle, Ready  };

    //小球原来的位置  
    private Vector3 m_BallOrigPosition;

    //左侧LineRenderer  
    private LineRenderer m_LineLeft;
    //右侧LineRenderer  
    private LineRenderer m_LineRight;

    private Sprite m_FoodSprite;

    private SlingshotState m_SlingshotState;

    public Vector2 m_BallMovingAreaLeftBottom;
    public Vector2 m_BallMovingAreaRightTop;

    void Start()
    {
        //获取LineRenderer  
        m_LineLeft = GameObject.Find("slingshot").transform.FindChild("ropeLeft").
            transform.GetComponent<LineRenderer>();
        m_LineRight = GameObject.Find("slingshot").transform.FindChild("ropeRight").
            transform.GetComponent<LineRenderer>();

        m_FoodSprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;

        m_BallOrigPosition = transform.position;

        m_SlingshotState = SlingshotState.Ready;
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
            Vector2 touchingPos = new Vector2(Input.touches[0].position.x, Input.touches[0].position.y);
            touchingPos = Camera.main.ScreenToWorldPoint(touchingPos);
            if (IsTouchingBall(touchingPos))
            {
                m_SlingshotState = SlingshotState.Ready;

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

        if (Input.GetMouseButtonUp(0))
        {
            //计算小球合力方向  
            Vector3 Vec3L = new Vector3(-2F - MousePos.x, 1.8F - MousePos.y, 0F - MousePos.z);
            Vector3 Vec3R = new Vector3(2F - MousePos.x, 1.8F - MousePos.y, 0F - MousePos.z);
            Vector3 Dir = (Vec3L + Vec3R).normalized;
            //获取刚体结构  
            transform.GetComponent<Rigidbody>().useGravity = true;
            transform.GetComponent<Rigidbody>().AddForce(Dir * 10F, ForceMode.Impulse);
            //恢复LineRenderer  
            LineL.SetPosition(0, new Vector3(0F, 1.8F, 0F));
            LineR.SetPosition(0, new Vector3(0F, 1.8F, 0F));
        }
    }

    protected void ChangeBallPosition()
    {
        Vector2 newBallPos = new Vector2(transform.position.x, transform.position.y);
        newBallPos += Input.touches[0].deltaPosition;
        transform.position = ClampTouchingPosition(newBallPos);
    }

    protected void ChangeRopePosition()
    {
        ChangeRopePosition(transform.position);
    }

    protected void ShotBall()
    {
        //TODO:
        Vector3 dir = m_BallOrigPosition - transform.position;
        transform.GetComponent<Rigidbody>().useGravity = true;
        transform.GetComponent<Rigidbody>().AddForce(Dir * 10F, ForceMode.Impulse);
    }

    protected void RopeRevert()
    {
        ChangeRopePosition(m_BallOrigPosition);
    }

    private void ChangeRopePosition(Vector3 pos)
    {
        m_LineLeft.SetPosition(1, pos);
        m_LineRight.SetPosition(1, pos);
    }
  

    protected bool IsTouchingBall(Vector2 touchingPosition)
    {
        bool ret = false;
        Vector2 leftBottom = new Vector2(
            transform.position.x - m_FoodSprite.rect.width * transform.lossyScale.x / 2,
            transform.position.y - m_FoodSprite.rect.height * transform.lossyScale.y / 2
            );
        Vector2 rightTop = new Vector2(
            transform.position.x + m_FoodSprite.rect.width * transform.lossyScale.x / 2,
            transform.position.y + m_FoodSprite.rect.height * transform.lossyScale.y / 2
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
        Vector3 ret = new Vector3(touchingPosition.x, touchingPosition.y, transform.position.z);
        ret.x = Mathf.Clamp(touchingPosition.x, m_BallMovingAreaLeftBottom.x, m_BallMovingAreaRightTop.x);
        ret.y = Mathf.Clamp(touchingPosition.y, m_BallMovingAreaLeftBottom.y, m_BallMovingAreaRightTop.y);
        return ret;
    }

    //void Update()
    //{
    //    if (Input.touchCount <= 0)
    //    {
    //        return;
    //    }

    //    if(Input.touchCount == 1)
    //    {
    //        if (Input.touches[0].phase == TouchPhase.Began)
    //        {

    //        }
    //    }
    //    if (Input.GetMouseButton(0))
    //    {
    //        //获取鼠标位置  
    //        MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -2F));
    //        //设置小球的位置  
    //        this.gameObject.transform.position = MousePos;
    //        //重新设置LineRenderer的位置  
    //        LineL.SetPosition(0, new Vector3(MousePos.x, MousePos.y, MousePos.z - 0.5F));
    //        LineR.SetPosition(0, new Vector3(MousePos.x, MousePos.y, MousePos.z - 0.5F));
    //    }
    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        //获取鼠标位置  
    //        MousePos = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -2F));
    //        //设置小球的位置  
    //        transform.position = MousePos;
    //        //重新设置LineRenderer的位置  
    //        LineL.SetPosition(0, new Vector3(MousePos.x, MousePos.y, MousePos.z - 0.5F));
    //        LineR.SetPosition(0, new Vector3(MousePos.x, MousePos.y, MousePos.z - 0.5F));

    //        //计算小球合力方向  
    //        Vector3 Vec3L = new Vector3(-2F - MousePos.x, 1.8F - MousePos.y, 0F - MousePos.z);
    //        Vector3 Vec3R = new Vector3(2F - MousePos.x, 1.8F - MousePos.y, 0F - MousePos.z);
    //        Vector3 Dir = (Vec3L + Vec3R).normalized;
    //        //获取刚体结构  
    //        transform.GetComponent<Rigidbody>().useGravity = true;
    //        transform.GetComponent<Rigidbody>().AddForce(Dir * 10F, ForceMode.Impulse);
    //        //恢复LineRenderer  
    //        LineL.SetPosition(0, new Vector3(0F, 1.8F, 0F));
    //        LineR.SetPosition(0, new Vector3(0F, 1.8F, 0F));
    //    }
    //}

    //protected bool IsTouchingFood(Vector3 touchPosition)
    //{
    //    bool ret = false;
    //    if( ((touchPosition.x > (transform.position.x - m_FoodSprite.)) && ()) )

    //    return ret;
    //}
}
