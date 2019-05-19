using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : BaseRangedWeapon
{
	[SerializeField] private float _maxForce = 15f;

	public override void Attack(BaseUnit unit = null)
	{
		transform.SetLocalZRotation(-45);
		Instantiate(_projectilePrefab, transform.position - transform.right, transform.rotation).Init(-Vector3.Distance(transform.position, unit.position), 1f, 2f, _projectilePathCurve);
	}
}
