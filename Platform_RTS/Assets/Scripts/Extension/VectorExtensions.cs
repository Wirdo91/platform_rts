using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtensions
{
	public static void SetLocalZRotation(this Transform transform, float amount)
	{
		Vector3 tmpRotation = transform.localEulerAngles;
		tmpRotation.z = amount;
		transform.localEulerAngles = tmpRotation;
	}
}
