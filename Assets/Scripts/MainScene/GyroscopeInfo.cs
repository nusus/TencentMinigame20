using UnityEngine;
using System.Collections;

public class GyroscopeInfo : MonoBehaviour {
    public float m_XDeltaSkewAngle;
    public float m_YDeltaSkewAngle;
    public float m_ZDeltaSkewAngle;

    private int m_FramesToUpdateRotateAngle;

    private RotateResult m_PhoneRotateResult;
    private Vector3 m_NewGravity;
    private float[] m_RotateAngleBuffer;

    private float m_LastRotateAngle;
    private float m_CurrentRotateAngle;


    /// <summary>
    /// FOR DEBUG
    /// </summary>
    /// 
    public bool m_IsDebug;
	// Use this for initialization
	void Start () {
        m_FramesToUpdateRotateAngle = 10;

        m_PhoneRotateResult = new RotateResult();
        m_NewGravity = new Vector3(0.0f, 0.0f, 0.0f);
        m_RotateAngleBuffer = new float[10];

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

        if( ((m_PhoneRotateResult.m_ZSkewAngle > - m_ZDeltaSkewAngle) && (m_PhoneRotateResult.m_ZSkewAngle < m_ZDeltaSkewAngle) ) 
            &&((m_PhoneRotateResult.m_XSkewAngle > 0)&&(m_PhoneRotateResult.m_XSkewAngle < 90))
            &&true    )
        {
            m_RotateAngleBuffer[9 - m_FramesToUpdateRotateAngle] = m_PhoneRotateResult.m_XSkewAngle;         
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
            m_CurrentRotateAngle /= 10;
            ChangeGravityDirection();

            m_FramesToUpdateRotateAngle = 10;
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
        Physics2D.gravity = new Vector2(-Mathf.Sin(m_CurrentRotateAngle), -Mathf.Cos(m_CurrentRotateAngle));
        //print(Physics.gravity);
    }
    
    public float GetRotateAngle()
    {
        float ret = m_CurrentRotateAngle - m_LastRotateAngle ;
        m_LastRotateAngle = m_CurrentRotateAngle;
        return ret;
    }  

}
