using UnityEngine;
using System.Collections;

public class Particle_Collision : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    void OnParticleCollision(GameObject other)
    {
        Destroy(other);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
