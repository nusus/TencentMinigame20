using UnityEngine;
using System.Collections;

public class BabyController : CircleMoveController {
    private int m_DrinkWaterFrames;
    public int m_FixedDrinkWaterFrames;
    private bool m_IsAnimationPlaying;

    private Animator m_BabyActionAnimator; 
    new public void Start()
    {
        base.Start();
        m_BabyActionAnimator = this.GetComponent<Animator>();

        m_DrinkWaterFrames = 0;
        m_IsAnimationPlaying = false;
    }


    protected override void RotateSelf()
    {
        base.RotateSelf();
        RotateBaby();

        //m_DrinkWaterFrames--;
        //if (m_DrinkWaterFrames < 0)
        //    if (m_IsAnimationPlaying)
        //    {
        //        m_BabyActionAnimator.SetBool("isDrinkWater", false);
        //        m_IsAnimationPlaying = false;
        //    }
       
    }

    protected void RotateBaby()
    {
        this.gameObject.transform.Rotate(Vector3.forward, m_GyroscopeInfo.GetRotateAngle());
    }

    public void OnParticleCollision(GameObject other)
    {
        //print("collition with particles");

        //m_DrinkWaterFrames = m_FixedDrinkWaterFrames;
        //if (!m_IsAnimationPlaying)
        //    m_BabyActionAnimator.SetBool("isDrinkWater", true);


    }

}
