using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.IO;

public class RotateResultTester {

	[Test]
	public void Test1()
	{
        Vector3 vector = new Vector3(-1 / Mathf.Sqrt(3), -1 / Mathf.Sqrt(3), -1 / Mathf.Sqrt(3));
        RotateResult rotate_result = new RotateResult();
        rotate_result.Update(vector);



        Assert.AreEqual(Mathf.Abs(rotate_result.m_XSkewAngle - (float)35.264) < 1e-3, true);
        Assert.AreEqual(Mathf.Abs(rotate_result.m_YSkewAngle - (float)35.264) < 1e-3, true);
        Assert.AreEqual(Mathf.Abs(rotate_result.m_ZSkewAngle - (float)35.264) < 1e-3, true);

    }

    [Test]
    public void Test2()
    {

        Vector3 vector = new Vector3(-1/Mathf.Sqrt(2), -1 / Mathf.Sqrt(2), 0);
        RotateResult rotate_result = new RotateResult();
        rotate_result.Update(vector);

        Assert.AreEqual(Mathf.Abs(rotate_result.m_XSkewAngle - 45) < 1e-3, true);
        Assert.AreEqual(Mathf.Abs(rotate_result.m_YSkewAngle - 45) < 1e-3, true);
        Assert.AreEqual(Mathf.Abs(rotate_result.m_ZSkewAngle) < 1e-3, true);
    }

    [Test]
    public void Test3()
    {

        Vector3 vector = new Vector3(0, 0, 1);
        RotateResult rotate_result = new RotateResult();
        rotate_result.Update(vector);

        Assert.AreEqual(Mathf.Abs(rotate_result.m_XSkewAngle) < 1e-3, true);
        Assert.AreEqual(Mathf.Abs(rotate_result.m_YSkewAngle) < 1e-3, true);
        Assert.AreEqual(Mathf.Abs(rotate_result.m_ZSkewAngle + 90) < 1e-3, true);
    }
}
