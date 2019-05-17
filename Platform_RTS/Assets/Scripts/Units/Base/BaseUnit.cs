using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUnit : MonoBehaviour
{
	public enum Team
	{
		Team1,
		Team2
	}

	[SerializeField] protected Team _team;
	public Team team => _team;

	public int health = 2;

	public BaseWeapon weapon = default;

	protected List<BaseUnit> _targetsInRange = new List<BaseUnit>();

	void Update()
    {
        if (IsCloseToEnemy())
		{
			Attack();
		}
		else
		{
			Move();
		}
	}
	private void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.GetComponent<BaseUnit>() is BaseUnit unitCollison)
		{
			if (unitCollison.team != team)
			{
				Debug.Log("test");
				_targetsInRange.Add(unitCollison);
			}
		}
		else if (collider.tag == "Weapon" && collider.gameObject.GetComponentInParent<BaseWeapon>() is BaseWeapon weaponHit)
		{
			Debug.Log("test");
			Damage(weaponHit.damage);
		}
		else
		{
			Debug.Log(collider.name);
		}
	}
	private void OnTriggerExit(Collider collider)
	{
		if (collider.gameObject.GetComponent<BaseUnit>() is BaseUnit unitCollison)
		{
			Debug.Log(collider.name);
			if (unitCollison.team != team)
			{
				_targetsInRange.Remove(unitCollison);
			}
		}
		else
		{
			Debug.Log(collider.name);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Projectile" && collision.gameObject.GetComponentInParent<Projectile>() is Projectile projectile)
		{
			Debug.Log("projectileHit");
			Damage(projectile.damage);
		}
	}
	
	protected virtual bool IsCloseToEnemy()
	{
		return _targetsInRange.Count > 0;
	}

	protected abstract void Move();

	protected abstract void Attack();

	public abstract void Damage(int amount);
}
