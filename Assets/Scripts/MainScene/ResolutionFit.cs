/*
 Resolution fit 
 let all cameras fit 1920 x 1080 if possible
 Won't effect UI.
 written by hearstzhang @ 2016.9.8
 */

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
