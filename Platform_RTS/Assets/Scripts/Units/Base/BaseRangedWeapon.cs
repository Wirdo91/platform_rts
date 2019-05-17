using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseRangedWeapon : BaseWeapon
{
	[SerializeField] protected Projectile _projectilePrefab = default;
}
