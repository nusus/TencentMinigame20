using UnityEngine;
using System.Collections;

public class GyroscopeInfo : MonoBehaviour {

    private Vector3 m_RotateAngle;
	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        int fingerCount = 0;
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
                fingerCount++;

        }
        if (fingerCount > 0)
            print(string.Format("x:{0}, y:{1}, z:{2}", Input.acceleration.x, Input.acceleration.y, Input.acceleration.z));
    }


    public float GetSinRotateAngle()
    {
        return -Input.acceleration.x;
    }

    public float GetCosRotateAngle()
    {
        return -Input.acceleration.y;
    }

    public 

}
