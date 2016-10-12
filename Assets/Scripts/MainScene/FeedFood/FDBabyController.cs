using UnityEngine;
using System.Collections;

public class FDBabyController : MonoBehaviour {
    public enum WalkingDirection { Left, Right};
    public float m_WalkingSpeed = 0.5f;
    public WalkingDirection m_WalkingDirection = WalkingDirection.Left;
    public float m_WalkingTime = 4.0f;

    private bool m_IsEatingFood = false;
    private Animator m_Animator;

    private FeedFoodGD m_GameDirector;
  
	// Use this for initialization
	void Start () {
        m_Animator = gameObject.GetComponent<Animator>();
        m_GameDirector = GameObject.Find("GameDirector").GetComponent<FeedFoodGD>();
        //m_Animator.SetInteger("walkingDirection", (int)m_WalkingDirection);

    }
	
	// Update is called once per frame
	void Update () {
        if (!m_IsEatingFood) {
            m_WalkingTime -= Time.deltaTime;
            int direction = m_WalkingDirection == WalkingDirection.Left ? -1 : 1;
            this.transform.Translate(direction * m_WalkingSpeed, 0.0f, 0.0f);
            if (m_WalkingTime <= 0.0f)
            {
                m_WalkingTime = 4.0f;
                m_WalkingDirection = (WalkingDirection)(((int)(m_WalkingDirection) + 1) & 1);
                m_Animator.SetInteger("walkingDirection", (int)m_WalkingDirection);
            }
        }
	
	}

    public void EatingFoodDone()
    {
        m_IsEatingFood = false;
        m_Animator.SetInteger("walkingDirection", (int)m_WalkingDirection);
    }

    void OnCollisionEnter(Collision coll)
    {
        print("collision");
        m_Animator.SetTrigger("eatFood");
        m_IsEatingFood = true;
        m_GameDirector.AddHitTimes();
        m_GameDirector.DecreaseLeftTimes();
        m_GameDirector.TryAgain();
    }

    void OnTriggerEnter(Collider coll)
    {
        print("trigger");
    }

    public void OnFoodBabyCollision()
    {
        //print("collision");
        m_Animator.SetTrigger("eatFood");
        //m_IsEatingFood = true;
        m_GameDirector.AddHitTimes();
        //m_GameDirector.DecreaseLeftTimes();
        m_GameDirector.TryAgain();
    }

}
