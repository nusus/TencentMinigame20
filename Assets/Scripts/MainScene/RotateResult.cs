using UnityEngine;
using System.Collections;

/// <summary>
/// 如果需要使用一些数学工具，例如反三角函数什么的，请参考unity文档，mathf
/// 设现实坐标系为A坐标系，手机坐标系为B坐标系，手机平放到桌面上时，AB坐标系重合。
/// 手机旋转时，手机坐标系也在旋转。
/// m_XBasisVector是A坐标系的X轴基向量在B坐标系下的位置。以此类推。
/// * m_XSkewAngle是A坐标系的X轴基向量与B坐标系XY平面的夹角。以此类推。
/// </summary>
public class RotateResult
{

    private Vector3 m_XBasisVector { get; set; }
    private Vector3 m_YBasisVector { get; set; }
    private Vector3 m_ZBasisVector { get; set; }


    /// <summary>
    /// 角度制，非弧度制
    /// </summary>
    private float m_XSkewAngle { get; set; }
    private float m_YSkewAngle { get; set; }
    private float m_ZSkewAngle { get; set; }


    /// <summary>
    /// 更新A坐标系下基向量在B坐标系下的坐标
    /// 更新A坐标系下基向量与相应平面的夹角
    /// </summary>
    /// <param name="newGravity">B坐标系下(0,0,-1)的向量在A坐标系下的坐标</param>
    public void Update(Vector3 newGravity)
    {

        Vector3 x = new Vector3(1, 0, 0);
        Vector3 y = new Vector3(0, 1, 0);
        Vector3 z = new Vector3(0, 0, 1);

        Vector3 gravity = dot(newGravity, 1 / norm(newGravity));

        Vector3 x_projection_on_gravity = dot(newGravity, dot(x, newGravity) / (norm(x) * norm(newGravity)));
        Vector3 new_x                   = minus(x, x_projection_on_gravity);
        new_x                           = dot(new_x, 1 / norm(new_x));

        Vector3 y_projection_on_gravity = dot(newGravity, dot(y, newGravity) / (norm(y) * norm(newGravity)));
        Vector3 y_projection_on_newx    = dot(new_x, dot(y, new_x) / (norm(y) * norm(new_x)));
        Vector3 new_y                   = minus(y, plus(y_projection_on_gravity, y_projection_on_newx));
        new_y                           = dot(new_y, 1 / norm(new_y));


        m_XBasisVector = new Vector3(dot(x,new_x), dot(x,new_y),dot(x,gravity));
        m_XBasisVector = dot(m_XBasisVector, 1 / norm(m_XBasisVector));
        m_YBasisVector = new Vector3(dot(y,new_x), dot(y,new_y),dot(y,gravity));
        m_YBasisVector = dot(m_YBasisVector, 1 / norm(m_YBasisVector));
        m_ZBasisVector = new Vector3(dot(z, new_x), dot(z, new_y), dot(z, gravity));
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
        return a.x * b.x + a.y * b.y + a.z * b.z;
    }
    private float norm(Vector3 a)
    {
        float dotresult = dot(a, a);
        if(Mathf.Abs(dotresult) < 1e-16)
        {
            return 0.0;
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

}
