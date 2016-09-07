using UnityEngine;
using System.Collections;
using System;

public class BottleController : CircleMoveController
{
    protected override Vector3 RotateCoordinate(Vector3 origVec3)
    {
        return RotateCoordinateImp(origVec3, ClockWise.ClockWise);
    }
}
