﻿using UnityEngine;
using System.Collections;
using System.IO;

/// <summary>
/// 如果需要使用一些数学工具，例如反三角函数什么的，请参考unity文档，mathf
/// 设现实坐标系为A坐标系，手机坐标系为B坐标系，手机平放到桌面上时，AB坐标系重合。
/// 手机旋转时，手机坐标系也在旋转。
/// m_XBasisVector是A坐标系的X轴基向量在B坐标系下的位置。以此类推。
/// * m_XSkewAngle是A坐标系的X轴基向量与B坐标系XY平面的夹角。以此类推。
/// </summary>
public class RotateResult
{
    private static float zero = (float)1e-6;
    public Vector3 m_XBasisVector;
    //public  Vector3 XBasisVector
    //{
    //    get
    //    {
    //        return m_XBasisVector;
    //    }
    //}
    public Vector3 m_YBasisVector;
    //public Vector3 YBasisVector
    //{
    //    get
    //    {
    //        return m_YBasisVector;
    //    }
    //}

    public Vector3 m_ZBasisVector;
    //public Vector3 ZBasisVector
    //{
    //    get
    //    {
    //        return m_ZBasisVector;
    //    }
    //}

    /// <summary>
    /// 角度制，非弧度制
    /// 返回值范围：-90-90,向上为正，向下为负
    /// </summary>
    public float m_XSkewAngle;
    //public float XSkewAngle
    //{
    //    get
    //    {
    //        return m_XSkewAngle;
    //    }

    //}

    public float m_YSkewAngle;
    //public float YSkewAngle
    //{
    //    get {
    //        return m_YSkewAngle;
    //    }
    //}

    public float m_ZSkewAngle;
    //public float ZSkewAngle
    //{
    //    get
    //    {
    //        return m_ZSkewAngle;
    //    }
    //}

    public RotateResult()
    {

    }

    /// <summary>
    /// 更新A坐标系下基向量在B坐标系下的坐标
    /// 更新A坐标系下基向量与相应平面的夹角
    /// </summary>
    /// <param name="newGravity">B坐标系下(0,0,-1)的向量在A坐标系下的坐标</param>
    public void Update(Vector3 newGravity)
    {

        //处理基向量x,y,z和重力向量重合的特殊情况
        if (Mathf.Abs(newGravity.x) < zero && Mathf.Abs(newGravity.y) < zero && Mathf.Abs(Mathf.Abs(newGravity.z) - 1) < zero)
        {
            newGravity.x += (float)zero;
            newGravity.y += (float)zero;
            newGravity.z -= 2*(float)zero;
        }
        if (Mathf.Abs(newGravity.x) < zero && Mathf.Abs(Mathf.Abs(newGravity.y) - 1) < zero && Mathf.Abs(newGravity.z) < zero)
        {
            newGravity.x += (float)zero;
            newGravity.y -= 2*(float)zero;
            newGravity.z += (float)zero;
        }
        if (Mathf.Abs(Mathf.Abs(newGravity.x) - 1)  < zero && Mathf.Abs(newGravity.y) < zero && Mathf.Abs(newGravity.z) < zero)
        {
            newGravity.x -= 2*(float)zero;
            newGravity.y += (float)zero;
            newGravity.z += (float)zero;
        }

        //A坐标系的基向量
        Vector3 x = new Vector3(1, 0, 0);
        Vector3 y = new Vector3(0, 1, 0);
        Vector3 z = new Vector3(0, 0, 1);

        Vector3 vertical_up             = dot(newGravity, -1 / norm(newGravity));

        Vector3 x_projection_on_up      = dot(vertical_up, dot(x, vertical_up) / (norm(x)));
        Vector3 new_x                   = minus(x, x_projection_on_up);
        new_x                           = dot(new_x, 1 / norm(new_x));

        Vector3 random   = new Vector3(Random.Range((float)0.0, (float)1.0), Random.Range((float)0.0, (float)1.0), Random.Range((float)0.0, (float)1.0));
        random           = dot(random, 1 / norm(random));
 

        Vector3 random_projection_on_up      = dot(vertical_up, dot(random, vertical_up) );
        Vector3 random_projection_on_newx    = dot(new_x, dot(random, new_x) );
        Vector3 new_y                   = minus(random, plus(random_projection_on_up, random_projection_on_newx));
        new_y                           = dot(new_y, 1 / norm(new_y));
 

        //x,y,z in B coordinate system
        m_XBasisVector = new Vector3(dot(x,new_x), dot(x,new_y),dot(x, vertical_up));
        m_XBasisVector = dot(m_XBasisVector, 1 / norm(m_XBasisVector));
        m_YBasisVector = new Vector3(dot(y,new_x), dot(y,new_y),dot(y, vertical_up));
        m_YBasisVector = dot(m_YBasisVector, 1 / norm(m_YBasisVector));
        m_ZBasisVector = new Vector3(dot(z, new_x), dot(z, new_y), dot(z, vertical_up));
        m_ZBasisVector = dot(m_ZBasisVector, 1 / norm(m_ZBasisVector));


        m_XSkewAngle = radians2degree(Mathf.Asin(m_XBasisVector.z));
        m_YSkewAngle = radians2degree(Mathf.Asin(m_YBasisVector.z));
        m_ZSkewAngle = radians2degree(Mathf.Asin(m_ZBasisVector.z));

    }

    //radians to degrees
    private float radians2degree(float radians)
    {
        return radians * 180 / Mathf.PI;
    }

    //linear algebra
    private Vector3 dot(Vector3 a, float f)
    {
        return new Vector3(a.x * f, a.y * f, a.z * f);
    }
    private float dot(Vector3 a, Vector3 b)
    {
        return Vector3.Dot(a, b);
    }
    private float norm(Vector3 a)
    {
        float dotresult = dot(a, a);
        if(Mathf.Abs(dotresult) < zero)
        {
            return (float)(zero);
        }else
        {
            return Mathf.Sqrt(dotresult);
        }
    }
    private Vector3 plus(Vector3 a, Vector3 b)
    {
        Vector3 c = new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
        return c;
    }
    private Vector3 minus(Vector3 a, Vector3 b)
    {
        Vector3 c = new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
        return c;
    }

    /*
    static void main(string[] args)
    {
        RotateResult result = new RotateResult();
        Vector3 test1 = new Vector3(1, 1, 1);
        result.Update(test1);

        System.Console.WriteLine(result.m_XSkewAngle);
    }
    */

}
