using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeUnit : BaseUnit
{
	[SerializeField] private Vector2 _targetLocation = default;
	[SerializeField] private float _moveSpeed = 5f;

	private BaseUnit _targetUnit = default;

	protected override void Attack()
	{
		if (team == Team.Team1)
		{
			_targetUnit?.Kill();
		}
		Debug.Log($"{name} ATTACK");
	}

	public override void Kill()
	{
		Destroy(gameObject);
	}

	protected override bool IsCloseToEnemy()
	{
		return _targetUnit != null;
	}

	protected override void Move()
	{
		if (Mathf.Abs(_targetLocation.x - transform.position.x) > 0.5f)
		{
			GetComponent<Rigidbody>().MovePosition(transform.position + new Vector3(((_targetLocation.x - transform.position.x) * _moveSpeed * Time.deltaTime), 0));
		}
	}

	private void OnTriggerStay(Collider collider)
	{
		if (collider.gameObject.GetComponent<BaseUnit>() is BaseUnit unitCollison)
		{
			if (unitCollison.team != team)
			{
				_targetUnit = unitCollison;
			}
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = team == Team.Team1 ? Color.green : Color.red;
		Gizmos.DrawLine(transform.position, _targetLocation);
		Gizmos.DrawCube(_targetLocation, Vector3.one * 0.5f);
	}
}
