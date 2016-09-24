/*
 IdleDetect
 used to detect idle circumstances.
 attach this to empty object which is always alive
 or main camera if it's sure to always alive.
 Copyright (C) 2016 hearstzhang all rights reserved.
 */

using UnityEngine;
using System.Collections;

public class IdleDetect : MonoBehaviour
{
    [Header("睡眠空闲时间")]
    [Tooltip("用于输入时间长度阈值，宠物空闲超过这个阈值则自动进入睡眠状态，单位为秒")]
    public float threshold;


    private float calcTime;

    // Use this for initialization
    void Start()
    {
        //Time in seconds since the start of the game.
        calcTime = Time.time;
    }

    void Update()
    {
        resetIdle();
        isIdle(threshold);
    }

    //call this every frame.
    //Todo: Considering to make it more efficient.
    bool isIdle(float threshold)
    {
        if (Time.time - calcTime >= threshold)
        {
            //return true means this has reached the threshold
            return true;
        }
        else
        {
            return false;
        }
    }

    //Todo: better use event listener instead. but anyway...
    //call this every frame.
    void resetIdle()
    {
        if (Input.touchCount >= 1)
        {
            calcTime = Time.time;
        }
        return;
    }
}
