/*
TouchManager.cs
Using to manage touch event 
Attach to empty object which is always alive,
or main camera if this is always alive.
Copyright (C) 2016 hearstzhang, all rights reserved.
 */

using UnityEngine;
using System.Collections;

public class TouchManager : MonoBehaviour
{
    [Tooltip("需要播放盖被动作的宠物")]
    [Header("动作执行者")]
    public GameObject baby;


    private float tempTime;
    Vector2 touchPos1, touchPos2, touchPos3;
    // Use this for initialization
    void Start()
    {
        tempTime = Time.time;
        touchPos1 = touchPos2 = touchPos3 = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 3 &&
            (Input.GetTouch(0).phase == TouchPhase.Began
            && Input.GetTouch(1).phase == TouchPhase.Began
            && Input.GetTouch(2).phase == TouchPhase.Began))
        {
            tempTime = Time.time;
            touchPos1 = Input.GetTouch(0).position;
            touchPos2 = Input.GetTouch(1).position;
            touchPos3 = Input.GetTouch(2).position;
            if (tempTime == Time.time + 1.0f
                && touchPos1.y < Input.GetTouch(0).position.y
                && touchPos2.y < Input.GetTouch(1).position.y
                && touchPos3.y < Input.GetTouch(2).position.y)
            {
                //Todo: play animations.

            }
        }
    }
}
