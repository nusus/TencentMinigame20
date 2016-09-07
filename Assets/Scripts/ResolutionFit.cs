using UnityEngine;
using System.Collections;

public class ResolutionFit : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        foreach(Camera camera in Camera.allCameras)
        {
            camera.aspect = 1920 / 1080f;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
