using UnityEngine;
using System.Collections;

public class CannonPlayAnimation : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void cannonPlay()
    {
        GetComponent<Animator>().SetTrigger("isShoot");
    }
}
