using UnityEngine;
using System.Collections;

public class GyroscopeInfo : MonoBehaviour {

    private float m_CurrentAngles;
    private float m_LastTargetAngles;

    public float m_AngularVelocity;
    public float m_DenoiseRange;
	// Use this for initialization
	void Start () {
        m_CurrentAngles = 0.0f;
        m_LastTargetAngles = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.acceleration.x >=0)
        {

        }

        float targetAngles = Mathf.Asin(-Input.acceleration.x);
        targetAngles = Denoise(m_LastTargetAngles, targetAngles, m_DenoiseRange);
        m_LastTargetAngles = targetAngles;
        m_CurrentAngles = AngularInterpolation(m_CurrentAngles, targetAngles, m_AngularVelocity);

        int fingerCount = 0;
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
                fingerCount++;

        }

        if (fingerCount > 0)
            print(targetAngles);

    }

    private float AngularInterpolation(float currentAngles, float targetAngles, float angleVelocity)
    {
        if (currentAngles + angleVelocity >= targetAngles)
        {
            return targetAngles;
        }
        return currentAngles + angleVelocity;
    }

    private float Denoise(float prev, float curr, float rang)
    {
        if (Mathf.Abs(curr - prev) <= rang)
        {
            return prev;
        }
        return curr;
    }


    public float GetSinRotateAngle()
    {
        return Mathf.Sin(m_CurrentAngles);
        //return -Input.acceleration.x;
        //return Mathf.Sin(m_RotateAngles);
    }

    public float GetCosRotateAngle()
    {
        return Mathf.Cos(m_CurrentAngles);
        //return -Input.acceleration.y;
        //return Mathf.Cos(m_RotateAngles);
    }

    public Vector3 RotateCoordinate(Vector3 origVec3)
    {
        Vector2 row1 = new Vector2(GetCosRotateAngle(), GetSinRotateAngle());
        Vector2 row2 = new Vector2(-GetSinRotateAngle(), GetCosRotateAngle());
        Vector2 coor = new Vector2(origVec3.x, origVec3.y);
        origVec3.x = Vector2.Dot(row1, coor);
        origVec3.y = Vector2.Dot(row2, coor);
        return origVec3;
    }

}
