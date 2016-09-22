using UnityEngine;
using System.Collections;
using System;

public class BottleController : CircleMoveController
{
    private ParticleSystem m_ParticleSystem;
    private bool m_IsParticleSystemPlayed;
    new public void Start()
    {
        base.Start();
        foreach(Transform ts in this.gameObject.transform)
        {
            if (ts.gameObject.name.Equals("water"))
            {
                m_ParticleSystem = ts.gameObject.GetComponent<ParticleSystem>();
                m_IsParticleSystemPlayed = false;
            }
        }       
    }

    protected override void RotateSelf()
    {
        base.RotateSelf();
        RotateWater();
    }

    protected void RotateWater()
    {
        this.gameObject.transform.Rotate(Vector3.forward, m_GyroscopeInfo.GetRotateAngle());
        if (m_GyroscopeInfo.GetCurrrentRotateAngle() > 30 && (!m_IsParticleSystemPlayed))
        {
            m_ParticleSystem.Play();
            m_IsParticleSystemPlayed = true;
        }
        if (m_GyroscopeInfo.GetCurrrentRotateAngle() <= 30 && m_IsParticleSystemPlayed)
        {
            m_ParticleSystem.Stop();
            m_IsParticleSystemPlayed = false;
        }
    }
}
