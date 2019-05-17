using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class BaseUnit : MonoBehaviour
{
	public enum Team
	{
		Team1 = -1,
		Team2 = 1
	}

	[SerializeField] protected Team _team;
	public Team team => _team;

	public Vector3 position => transform.position;

	public abstract float range { get; protected set; }

	public int health = 2;

	public BaseWeapon weapon = default;

	protected List<BaseUnit> _targetsInRange = new List<BaseUnit>();
	protected Rigidbody _rigidbody = default;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		WorldManager.AddUnit(this);
	}

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
		if (collider.tag == "Weapon" && collider.gameObject.GetComponentInParent<BaseWeapon>() is BaseWeapon weaponHit)
		{
			Debug.Log("test");
			Damage(weaponHit.damage);
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

	private void UpdateTargetsInRange()
	{
		_targetsInRange = WorldManager.GetEnemiesInRange(this);
	}
	
	protected virtual bool IsCloseToEnemy()
	{
		UpdateTargetsInRange();
		return _targetsInRange.Count > 0;
	}

	protected abstract void Move();

	protected abstract void Attack();

	public abstract void Damage(int amount);
}
