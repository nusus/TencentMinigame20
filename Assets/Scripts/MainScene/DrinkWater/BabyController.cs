using UnityEngine;
using System.Collections;

public class BabyController : CircleMoveController {
    private int m_DrinkWaterFrames;
    public int m_FixedDrinkWaterFrames;
    private bool m_IsAnimationPlaying;

    private Animator m_BabyActionAnimator;

    private DrinkWaterGD m_GameDirector;
    new public void Start()
    {
        base.Start();
        m_BabyActionAnimator = this.GetComponent<Animator>();
        m_GameDirector = GameObject.Find("GameDirector").GetComponent<DrinkWaterGD>();

        m_DrinkWaterFrames = 0;
        m_IsAnimationPlaying = false;
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
        m_GameDirector.IncreaseDrinkWaterSeconds(Time.fixedDeltaTime);

    }

}
