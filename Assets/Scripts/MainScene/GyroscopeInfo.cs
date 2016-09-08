using UnityEngine;
using System.Collections;

public class GyroscopeInfo : MonoBehaviour {

    private float m_CurrentTargetAngles;
    private float m_LastTargetAngles;

    public float m_DenosieThreshold;
	// Use this for initialization
	void Start () {
        m_CurrentTargetAngles = Mathf.Asin(-Input.acceleration.x);
        m_LastTargetAngles = m_CurrentTargetAngles;
    }

    void FixedUpdate()
    {
        ChangeGravityDirection();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.acceleration.x <= 0.0f && Input.acceleration.y <=0.0f)
        {
            float targetAngles = Mathf.Asin(-Input.acceleration.x);
            targetAngles = Denoise(m_CurrentTargetAngles, targetAngles, m_DenosieThreshold);
            m_LastTargetAngles = m_CurrentTargetAngles;
            m_CurrentTargetAngles = targetAngles;

            int fingerCount = 0;
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
                    fingerCount++;
            }

            if (fingerCount > 0)
                print(targetAngles);

            //ChangeGravityDirection();
        }
    }

    private void ChangeGravityDirection()
    {
        Physics2D.gravity = new Vector2(Input.acceleration.x, Input.acceleration.y);
        //print(Physics.gravity);
    }

    private float Denoise(float prev, float curr, float rang)
    {
        if (Mathf.Abs(curr - prev) <= rang)
        {
            return prev;
        }
        return curr;
    }
    
    public float GetRotateAngle()
    {
        return -(m_CurrentTargetAngles - m_LastTargetAngles) * Mathf.Rad2Deg ;
    }  

}
