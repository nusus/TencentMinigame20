using UnityEngine;
using System.Collections;

public class CircleMoveController : MonoBehaviour {
    public enum ClockWise {ClockWise, CounterClockWise};

    public GyroscopeInfo m_GyroscopeInfo;

    // Use this for initialization
    public void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        this.RotateCoordinate();

    }

    protected void RotateCoordinate()
    {
        this.gameObject.transform.RotateAround(Vector3.zero, Vector3.forward, m_GyroscopeInfo.GetRotateAngle());
    }
}
