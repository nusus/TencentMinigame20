using UnityEngine;
using System.Collections;

public class BabyController : MonoBehaviour {
    public GyroscopeInfo m_GyroscopeInfo;

    private Vector3 m_OrignalPosition;
	// Use this for initialization
	void Start () {
        m_OrignalPosition = this.gameObject.transform.position;
	
	}
	
	// Update is called once per frame
	void Update () {
        this.gameObject.transform.position = m_GyroscopeInfo.RotateCoordinate(m_OrignalPosition);
     
	}
}
