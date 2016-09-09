using UnityEngine;
using System.Collections;

public class BabyController : CircleMoveController {
    private Animator m_BabyActionAnimator; 
    new public void Start()
    {
        base.Start();
        m_BabyActionAnimator = this.GetComponent<Animator>();
    }
    new public void Update()
    {
        base.Update();
        RotateSelf();
    }

    protected void RotateSelf()
    {
        this.gameObject.transform.Rotate(Vector3.forward, - m_GyroscopeInfo.GetRotateAngle());
    }

    void OnParticleCollision(GameObject other)
    {
        Destroy(other);
        m_BabyActionAnimator.SetBool("isDrinkWater", true);
    }

}
