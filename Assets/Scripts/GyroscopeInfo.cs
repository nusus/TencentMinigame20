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

        ChangeGravityDirection();
        //print(targetAngles);
    }

    private void ChangeGravityDirection()
    {
        Physics.gravity = new Vector3(Input.acceleration.x, Input.acceleration.y, Input.acceleration.z);
        //print(Physics.gravity);
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
    }

    public float GetCosRotateAngle()
    {
        return Mathf.Cos(m_CurrentAngles);
    }  

}
