using UnityEngine;
using System.Collections;

public class ResolusionFit : MonoBehaviour {

	// Use this for initialization
	void Start () {
        foreach(Camera camera in Camera.allCameras)
        {
            camera.aspect = 1440 / 900f;
        }
	}

}
