﻿using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour
{
    protected enum SlingshotState { Idle, Ready  };

    //鼠标位置  
    private Vector3 m_TouchPosition;

    //左侧LineRenderer  
    private LineRenderer m_LineLeft;
    //右侧LineRenderer  
    private LineRenderer m_LineRight;

    private Sprite m_FoodSprite;

    void Start()
    {
        //获取LineRenderer  
        m_LineLeft = GameObject.Find("slingshot").transform.FindChild("ropeLeft").
            transform.GetComponent<LineRenderer>();
        m_LineRight = GameObject.Find("slingshot").transform.FindChild("ropeRight").
            transform.GetComponent<LineRenderer>();

        m_FoodSprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;
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
