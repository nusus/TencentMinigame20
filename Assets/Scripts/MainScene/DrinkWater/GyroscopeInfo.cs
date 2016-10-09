using UnityEngine;
using System.Collections;

public class GyroscopeInfo : MonoBehaviour {
    public float m_XDeltaSkewAngle = 0;
    public float m_YDeltaSkewAngle = 0;
    public float m_ZDeltaSkewAngle = 10;

    private int m_FramesToUpdateRotateAngle;
    public int m_FixedFramesToUpdateRotateAngle = 6;

    private RotateResult m_PhoneRotateResult;
    private Vector3 m_NewGravity;
    private float[] m_RotateAngleBuffer;

    private float m_LastRotateAngle;
    private float m_CurrentRotateAngle;

    private float m_CurrentBottleAngle;

    private bool m_IsRotateAngleToBeUpdated;

    private float m_DeltaAngle;

    public float m_FixedAngleSpeed = 2;
    /// <summary>
    /// FOR DEBUG
    /// </summary>
    /// 
    public bool m_IsDebug = true;
	// Use this for initialization
	void Start () {
        //version 1
        //m_FramesToUpdateRotateAngle = m_FixedFramesToUpdateRotateAngle;

        //m_IsRotateAngleToBeUpdated = true;

        //m_PhoneRotateResult = new RotateResult();
        //m_NewGravity = new Vector3(0.0f, 0.0f, 0.0f);
        //m_RotateAngleBuffer = new float[m_FixedFramesToUpdateRotateAngle];

        //version 2 
        m_FramesToUpdateRotateAngle = 0;
        m_CurrentBottleAngle = 0.0f;


        m_IsRotateAngleToBeUpdated = true;

        m_PhoneRotateResult = new RotateResult();
        m_NewGravity = new Vector3(0.0f, 0.0f, 0.0f);
        m_RotateAngleBuffer = new float[m_FixedFramesToUpdateRotateAngle];
        for (int i = 0; i< m_FixedFramesToUpdateRotateAngle; ++i)
        {
            m_RotateAngleBuffer[i] = 0.0f;
        }
    }

    void FixedUpdate()
    {
        ChangeGravityDirection();
    }

    // Update is called once per frame
    void Update()
    {
        //version 1 
        //m_NewGravity[0] = Input.acceleration.x;
        //m_NewGravity[1] = Input.acceleration.y;
        //m_NewGravity[2] = Input.acceleration.z;
        //m_PhoneRotateResult.Update(m_NewGravity);

        //m_FramesToUpdateRotateAngle--;

        //m_IsRotateAngleToBeUpdated = false;

        //if ( ((m_PhoneRotateResult.m_ZSkewAngle > - m_ZDeltaSkewAngle) && (m_PhoneRotateResult.m_ZSkewAngle < m_ZDeltaSkewAngle) ) 
        //    //&&((m_PhoneRotateResult.m_XSkewAngle > 0) && (m_PhoneRotateResult.m_XSkewAngle < 90))
        //    //&& (m_PhoneRotateResult.m_YSkewAngle > 0)
        //    &&true    )
        //{            
        //    float XSkewAngle = m_PhoneRotateResult.m_XSkewAngle;

        //    if (XSkewAngle > 90)
        //    {
        //        XSkewAngle = 90;
        //    }

        //    if (XSkewAngle < 0) XSkewAngle = 0;

        //    if (m_PhoneRotateResult.m_XSkewAngle > 0 && m_PhoneRotateResult.m_YSkewAngle < 0)
        //    {
        //        XSkewAngle = 90;
        //    }

        //    m_RotateAngleBuffer[m_FixedFramesToUpdateRotateAngle - 1 - m_FramesToUpdateRotateAngle] = XSkewAngle;
        //}

        ////denoise the phone_rotate_angle
        //if ( 0 == m_FramesToUpdateRotateAngle)
        //{
        //    m_LastRotateAngle = m_CurrentRotateAngle;
        //    m_CurrentRotateAngle = 0.0f;
        //    for (int i = 0; i < m_RotateAngleBuffer.Length; i++)
        //    {
        //        m_CurrentRotateAngle += m_RotateAngleBuffer[i];
        //    }
        //    m_CurrentRotateAngle /= m_FixedFramesToUpdateRotateAngle;
        //    ChangeGravityDirection();

        //    m_FramesToUpdateRotateAngle = m_FixedFramesToUpdateRotateAngle;

        //    m_IsRotateAngleToBeUpdated = true;
        //}


        //if(m_IsDebug)
        //{
        //    int fingerCount = 0;
        //    foreach (Touch touch in Input.touches)
        //    {
        //        if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
        //            fingerCount++;
        //    }

        //    if (fingerCount > 0)
        //        print(m_CurrentRotateAngle);
        //}


        //version 2
        //m_NewGravity[0] = Input.acceleration.x;
        //m_NewGravity[1] = Input.acceleration.y;
        //m_NewGravity[2] = Input.acceleration.z;
        //m_PhoneRotateResult.Update(m_NewGravity);

        //m_FramesToUpdateRotateAngle++;

        //m_IsRotateAngleToBeUpdated = false;

        //if (((m_PhoneRotateResult.m_ZSkewAngle > -m_ZDeltaSkewAngle) && (m_PhoneRotateResult.m_ZSkewAngle < m_ZDeltaSkewAngle))
        //    //&&((m_PhoneRotateResult.m_XSkewAngle > 0) && (m_PhoneRotateResult.m_XSkewAngle < 90))
        //    //&& (m_PhoneRotateResult.m_YSkewAngle > 0)
        //    && true)
        //{
        //    float XSkewAngle = m_PhoneRotateResult.m_XSkewAngle;

        //    if (XSkewAngle > 90)
        //    {
        //        XSkewAngle = 90;
        //    }

        //    if (XSkewAngle < 0) XSkewAngle = 0;

        //    if (m_PhoneRotateResult.m_XSkewAngle > 0 && m_PhoneRotateResult.m_YSkewAngle < 0)
        //    {
        //        XSkewAngle = 90;
        //    }

        //    m_RotateAngleBuffer[m_FramesToUpdateRotateAngle % m_FixedFramesToUpdateRotateAngle] = XSkewAngle;
        //}


        //m_LastRotateAngle = m_CurrentRotateAngle;
        //m_CurrentRotateAngle = 0.0f;
        //for (int i = 0; i < m_RotateAngleBuffer.Length; i++)
        //{
        //    m_CurrentRotateAngle += m_RotateAngleBuffer[i];
        //}
        //m_CurrentRotateAngle /= m_FixedFramesToUpdateRotateAngle;

        //m_IsRotateAngleToBeUpdated = true;


        //if (m_IsDebug)
        //{
        //    int fingerCount = 0;
        //    foreach (Touch touch in Input.touches)
        //    {
        //        if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
        //            fingerCount++;
        //    }

        //    if (fingerCount > 0)
        //        print(m_CurrentRotateAngle);
        //}


        //version 3
        m_NewGravity[0] = Input.acceleration.x;
        m_NewGravity[1] = Input.acceleration.y;
        m_NewGravity[2] = Input.acceleration.z;
        m_PhoneRotateResult.Update(m_NewGravity);

        m_FramesToUpdateRotateAngle++;

        m_IsRotateAngleToBeUpdated = false;

        if (((m_PhoneRotateResult.m_ZSkewAngle > -m_ZDeltaSkewAngle) && (m_PhoneRotateResult.m_ZSkewAngle < m_ZDeltaSkewAngle))
            //&&((m_PhoneRotateResult.m_XSkewAngle > 0) && (m_PhoneRotateResult.m_XSkewAngle < 90))
            //&& (m_PhoneRotateResult.m_YSkewAngle > 0)
            && true)
        {
            float XSkewAngle = m_PhoneRotateResult.m_XSkewAngle;

            if (XSkewAngle > 90)
            {
                XSkewAngle = 90;
            }

            if (XSkewAngle < 0) XSkewAngle = 0;

            if (m_PhoneRotateResult.m_XSkewAngle > 0 && m_PhoneRotateResult.m_YSkewAngle < 0)
            {
                XSkewAngle = 90;
            }

            m_RotateAngleBuffer[m_FramesToUpdateRotateAngle % m_FixedFramesToUpdateRotateAngle] = XSkewAngle;
        }


        m_LastRotateAngle = m_CurrentRotateAngle;
        m_CurrentRotateAngle = 0.0f;
        for (int i = 0; i < m_RotateAngleBuffer.Length; i++)
        {
            m_CurrentRotateAngle += m_RotateAngleBuffer[i];
        }
        m_CurrentRotateAngle /= m_FixedFramesToUpdateRotateAngle;

        m_IsRotateAngleToBeUpdated = true;

        float ret = m_CurrentRotateAngle - m_CurrentBottleAngle > 0 ? m_FixedAngleSpeed : -m_FixedAngleSpeed;
        if (ret > 0)
        {
            if (m_CurrentBottleAngle + ret > m_CurrentRotateAngle)
            {
                ret = m_CurrentRotateAngle - m_CurrentBottleAngle;
            }
        }
        else
        {
            if (m_CurrentBottleAngle + ret < m_CurrentRotateAngle)
            {
                ret = m_CurrentRotateAngle - m_CurrentBottleAngle;
            }
        }
        m_CurrentBottleAngle += ret;
        //m_LastRotateAngle = m_CurrentRotateAngle;

        m_DeltaAngle = ret;

        if (m_IsDebug)
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
        //version 2
        //float ret = m_CurrentRotateAngle - m_LastRotateAngle;
        ////m_LastRotateAngle = m_CurrentRotateAngle;
        //return ret;

        ////version 3
        //float ret = m_CurrentRotateAngle - m_CurrentBottleAngle > 0 ? m_FixedAngleSpeed : -m_FixedAngleSpeed;
        //if (ret > 0)
        //{
        //    if (m_CurrentBottleAngle + m_FixedAngleSpeed > m_CurrentRotateAngle)
        //    {
        //        ret = m_CurrentRotateAngle - m_CurrentBottleAngle;
        //    }
        //}
        //else
        //{
        //    if (m_CurrentBottleAngle + m_FixedAngleSpeed < m_CurrentRotateAngle)
        //    {
        //        ret = m_CurrentRotateAngle - m_CurrentBottleAngle;
        //    }
        //}
        //m_CurrentBottleAngle += ret;
        ////m_LastRotateAngle = m_CurrentRotateAngle;
        //return ret;

        return m_DeltaAngle;
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
