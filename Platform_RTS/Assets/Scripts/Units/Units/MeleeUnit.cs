using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeUnit : BaseUnit
{
	[SerializeField] private Vector2 _targetLocation = default;
	[SerializeField] private float _moveSpeed = 5f;

	public override float range {
		get => 1;
		protected set => throw new System.NotImplementedException(); }

	protected override void Attack()
	{
		weapon?.Attack();
	}

	public override void Damage(int amount)
	{
		health -= amount;

		if (health <= 0)
		{
			this.enabled = false;
			GetComponent<BoxCollider>().enabled = false;
			GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
			GetComponent<Rigidbody>().AddForceAtPosition(Vector3.back * 50, transform.position + Vector3.up);
		}
	}

	protected override void Move()
	{
		if (Mathf.Abs(_targetLocation.x - transform.position.x) > 0.5f)
		{
			GetComponent<Rigidbody>().MovePosition(transform.position + new Vector3((Mathf.Sign(_targetLocation.x - transform.position.x) * _moveSpeed * Time.deltaTime), 0));
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = team == Team.Team1 ? Color.green : Color.red;
		Gizmos.DrawLine(transform.position, _targetLocation);
		Gizmos.DrawCube(_targetLocation, Vector3.one * 0.5f);
	}
}
