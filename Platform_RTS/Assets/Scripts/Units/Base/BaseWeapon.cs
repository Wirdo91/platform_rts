using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
	[SerializeField] protected int _damage;
	public int damage => _damage;

	public abstract void Attack(BaseUnit unit = null);
}
