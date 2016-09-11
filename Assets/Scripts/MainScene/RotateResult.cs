using UnityEngine;
using System.Collections;


/// <summary>
/// 如果需要使用一些数学工具，例如反三角函数什么的，请参考unity文档，mathf
/// 设现实坐标系为A坐标系，手机坐标系为B坐标系，手机平放到桌面上时，AB坐标系重合。
/// 手机旋转时，手机坐标系也在旋转。
/// m_XBasisVector是A坐标系的X轴基向量在B坐标系下的位置。以此类推。
/// * m_XSkewAngle是A坐标系的X轴基向量与B坐标系XY平面的夹角。以此类推。
/// </summary>
public class RotateResult {    

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
        //TODO: to be implemented
    }
}

