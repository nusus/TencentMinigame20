using UnityEngine;
using System.Collections;

abstract public class CircleMoveController : MonoBehaviour {
    public enum ClockWise {ClockWise, CounterClockWise};

    public GyroscopeInfo m_GyroscopeInfo;

    protected Vector3 m_OrignalPosition;
    // Use this for initialization
    void Start()
    {
        m_OrignalPosition = this.gameObject.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = this.RotateCoordinate(m_OrignalPosition);

    }

    abstract protected Vector3 RotateCoordinate(Vector3 origVec3);

    protected Vector3 RotateCoordinateImp(Vector3 origVec3, ClockWise clockWise)
    {
        int wise = clockWise == ClockWise.ClockWise ? -1 : 1;
        Vector2 row1 = new Vector2(m_GyroscopeInfo.GetCosRotateAngle(), -wise * m_GyroscopeInfo.GetSinRotateAngle());
        Vector2 row2 = new Vector2(wise * m_GyroscopeInfo.GetSinRotateAngle(), m_GyroscopeInfo.GetCosRotateAngle());
        Vector2 coor = new Vector2(origVec3.x, origVec3.y);
        origVec3.x = Vector2.Dot(row1, coor);
        origVec3.y = Vector2.Dot(row2, coor);
        return origVec3;
    }
}
