using UnityEngine;
using System.Collections;

public class BabyController : CircleMoveController {
    private Animator m_BabyActionAnimator;
    public float m_NotDrinkingWaterTime = 2.0f;
    private bool m_IsDrinkingWater = false;
    private bool m_IsFat = false;

    private DrinkWaterGD m_GameDirector;
    new public void Start()
    {
        base.Start();
        m_BabyActionAnimator = this.GetComponent<Animator>();
        m_GameDirector = GameObject.Find("GameDirector").GetComponent<DrinkWaterGD>();
    }

    new public void Update()
    {
        base.Update();
        m_NotDrinkingWaterTime -= Time.deltaTime;
        if (m_NotDrinkingWaterTime < 0) {
            m_IsDrinkingWater = false;
        }

        if (m_GameDirector.GetDrinkingWaterPercent() > 40)
        {
            m_IsFat = true;
        }

        if (!m_IsDrinkingWater) {
            if(m_IsFat)
            {
                m_BabyActionAnimator.SetTrigger("fatNotDrinking");
            }
            else
            {
                m_BabyActionAnimator.SetTrigger("slimNotDrinking");
            }
        }

    }

    protected override void RotateSelf()
    {
        base.RotateSelf();
        RotateBaby();     
    }

    protected void RotateBaby()
    {
        this.gameObject.transform.Rotate(Vector3.forward, m_GyroscopeInfo.GetRotateAngle());
    }

    public void OnParticleCollision(GameObject other)
    {
        m_GameDirector.IncreaseDrinkWaterSeconds(Time.fixedDeltaTime * 6);
        m_NotDrinkingWaterTime = 2.0f;
        m_IsDrinkingWater = true;
        m_BabyActionAnimator.SetTrigger("drinkingWater");
    }

}
