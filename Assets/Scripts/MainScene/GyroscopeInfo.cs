using UnityEngine;
using System.Collections;

public class GyroscopeInfo : MonoBehaviour {
    public float m_XDeltaSkewAngle;
    public float m_YDeltaSkewAngle;
    public float m_ZDeltaSkewAngle;

    private int m_FramesToUpdateRotateAngle;
    public int m_FixedFramesToUpdateRotateAngle;

    private RotateResult m_PhoneRotateResult;
    private Vector3 m_NewGravity;
    private float[] m_RotateAngleBuffer;

    private float m_LastRotateAngle;
    private float m_CurrentRotateAngle;

    private bool m_IsRotateAngleToBeUpdated;
    /// <summary>
    /// FOR DEBUG
    /// </summary>
    /// 
    public bool m_IsDebug;
	// Use this for initialization
	void Start () {
        m_FramesToUpdateRotateAngle = m_FixedFramesToUpdateRotateAngle;

        m_IsRotateAngleToBeUpdated = true;

        m_PhoneRotateResult = new RotateResult();
        m_NewGravity = new Vector3(0.0f, 0.0f, 0.0f);
        m_RotateAngleBuffer = new float[m_FixedFramesToUpdateRotateAngle];

    }

    void FixedUpdate()
    {
        ChangeGravityDirection();
    }

    // Update is called once per frame
    void Update()
    {
        m_NewGravity[0] = Input.acceleration.x;
        m_NewGravity[1] = Input.acceleration.y;
        m_NewGravity[2] = Input.acceleration.z;
        m_PhoneRotateResult.Update(m_NewGravity);

        m_FramesToUpdateRotateAngle--;

        m_IsRotateAngleToBeUpdated = false;

        if ( ((m_PhoneRotateResult.m_ZSkewAngle > - m_ZDeltaSkewAngle) && (m_PhoneRotateResult.m_ZSkewAngle < m_ZDeltaSkewAngle) ) 
            //&&((m_PhoneRotateResult.m_XSkewAngle > 0) && (m_PhoneRotateResult.m_XSkewAngle < 90))
            && (m_PhoneRotateResult.m_YSkewAngle > 0)
            &&true    )
        {            
            float XSkewAngle = m_PhoneRotateResult.m_XSkewAngle;
            if (XSkewAngle > 90)
            {
                XSkewAngle = 90;
            }

            if (XSkewAngle < 0) XSkewAngle = 0;
            m_RotateAngleBuffer[m_FixedFramesToUpdateRotateAngle - 1 - m_FramesToUpdateRotateAngle] = XSkewAngle;
        }

        //denoise the phone_rotate_angle
        if ( 0 == m_FramesToUpdateRotateAngle)
        {
            m_LastRotateAngle = m_CurrentRotateAngle;
            m_CurrentRotateAngle = 0.0f;
            for (int i = 0; i < m_RotateAngleBuffer.Length; i++)
            {
                m_CurrentRotateAngle += m_RotateAngleBuffer[i];
            }
            m_CurrentRotateAngle /= m_FixedFramesToUpdateRotateAngle;
            ChangeGravityDirection();

            m_FramesToUpdateRotateAngle = m_FixedFramesToUpdateRotateAngle;

            m_IsRotateAngleToBeUpdated = true;
        }


        if(m_IsDebug)
        {
            int fingerCount = 0;
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
                    fingerCount++;
            }

            if (fingerCount > 0)
                print(m_CurrentRotateAngle);
        }
    }

    private void ChangeGravityDirection()
    {
        //Physics2D.setGravity(new Vector2(-Mathf.Sin(m_CurrentRotateAngle), -Mathf.Cos(m_CurrentRotateAngle)));
        //print(Physics.gravity);
    }
    
    public float GetRotateAngle()
    {
        float ret = m_CurrentRotateAngle - m_LastRotateAngle ;
        //m_LastRotateAngle = m_CurrentRotateAngle;
        return ret;
    }
    
    public float GetCurrrentRotateAngle()
    {
        return m_CurrentRotateAngle;
    }
    
    public bool IsRotateAngleNeededToBeUpdate()
    {
        return m_IsRotateAngleToBeUpdated;
    }  

}
