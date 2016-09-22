﻿using UnityEngine;
using System.Collections;

public class ShackPhoneDetect : MonoBehaviour {

    // basic parameters.
    private float accelerometerUpdateInterval = 1.0f/60.0f;
    private float lowPassKernelWidthInSeconds = 1.0f;
    private float shakeDetectionThreshold = 2.0f;

    private float lowPassFilterFactor = 0f;
    private Vector3 lowPassValue = Vector3.zero;
    private Vector3 acceleration = Vector3.zero;
    private Vector3 deltaAcceleration = Vector3.zero;


    // Use this for initialization
    void Start()
    {
        lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
        lowPassValue = Input.acceleration;
    }

    // Update is called once per frame
    void Update()
    {
        acceleration = Input.acceleration;
        lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
        deltaAcceleration = acceleration - lowPassValue;
        if(deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold)
        {
            Debug.Log("Shake event detected at time " + Time.time);
        }
    }
}
