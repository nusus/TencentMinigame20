using UnityEngine;
using System.Collections;
using System;

public class BottleController : CircleMoveController
{
    new public void Update()
    {
        base.Update();
        RotateWater();
    }

    protected void RotateWater()
    {
        this.gameObject.transform.Rotate(Vector3.forward, -m_GyroscopeInfo.GetRotateAngle());
    }
}
