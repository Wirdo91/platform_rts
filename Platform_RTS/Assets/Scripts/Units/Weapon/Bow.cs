using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : BaseRangedWeapon
{
	private float _maxForce = 2f;

	public override void Attack(BaseUnit unit = null)
	{
		Instantiate(_projectilePrefab, transform.position - transform.right, transform.rotation).Init(_maxForce);
	}
}
