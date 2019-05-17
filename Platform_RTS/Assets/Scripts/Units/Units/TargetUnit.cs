using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetUnit : BaseUnit
{
	public override void Damage(int amount)
	{
	}

	protected override void Attack()
	{
	}

	protected override bool IsCloseToEnemy()
	{
		return false;
	}

	protected override void Move()
	{
	}
}
