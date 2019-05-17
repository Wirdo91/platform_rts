using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedUnit : BaseUnit
{
	[SerializeField] private Vector2 _targetLocation = default;
	[SerializeField] private float _moveSpeed = 5f;

	public override float range {
		get => 20;
		protected set => throw new System.NotImplementedException(); }

	public override void Damage(int amount)
	{
		health -= amount;

		if (health <= 0)
		{
			this.enabled = false;
			GetComponent<BoxCollider>().enabled = false;
			_rigidbody.constraints = RigidbodyConstraints.None;
			_rigidbody.AddForceAtPosition(Vector3.back * 50, transform.position + Vector3.up);
		}
	}

	private float _attackTimer = 0;

	protected override void Attack()
	{
		if (_attackTimer <= 0)
		{
			weapon?.Attack(_targetsInRange[0]);
			_attackTimer = _attacksPerSecond / 1;
		}
		else
		{
			_attackTimer -= Time.deltaTime;
		}
	}

	protected override void Move()
	{
		if (Mathf.Abs(_targetLocation.x - transform.position.x) > 0.5f)
		{
			_rigidbody.MovePosition(transform.position + new Vector3((Mathf.Sign(_targetLocation.x - transform.position.x) * _moveSpeed * Time.deltaTime), 0));
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = team == Team.Team1 ? Color.green : Color.red;
		Gizmos.DrawLine(transform.position, _targetLocation);
		Gizmos.DrawCube(_targetLocation, Vector3.one * 0.5f);
	}
}
