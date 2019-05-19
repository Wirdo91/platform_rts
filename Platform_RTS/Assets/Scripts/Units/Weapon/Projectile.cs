using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
	[SerializeField] private int _damage = 1;
	public int damage => _damage;

	[SerializeField] private bool _stickToModel = false;
	public bool stickToModel => _stickToModel;

	private Rigidbody _rigidbody = default;

	private float _distanceToTarget = default;
	private float _flightTime = default;
	private float _maxDeltaHeight = default;

	private Vector3 _startPosition = default;

	private AnimationCurve _flightCurve = default;

	private bool _dead = false;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_rigidbody.isKinematic = true;
		_rigidbody.useGravity = false;
	}

	public void Init(float distance, float flightTime, float maxDeltaHeight, AnimationCurve flightCurve)
	{
		_distanceToTarget = distance;
		_flightTime = flightTime;
		_maxDeltaHeight = maxDeltaHeight;
		_flightCurve = flightCurve;
		_startPosition = _rigidbody.transform.position;
	}

	private void FixedUpdate()
	{
		if (!_dead)
		{
			UpdateMovement();
			RotationChangeWhileFlying();
		}
	}

	private float _currentFlightTime = 0f;
	private Vector3 _prevPosition;
	private bool _flightEnded = false;
	void UpdateMovement()
	{
		if (_currentFlightTime < _flightTime)
		{
			Vector3 newPosition = _rigidbody.position;
			_prevPosition = newPosition;

			float timeStamp = _currentFlightTime / _flightTime;

			newPosition.y = _startPosition.y + (_maxDeltaHeight * _flightCurve.Evaluate(timeStamp));
			newPosition.x = _startPosition.x + ((_distanceToTarget + 3) * timeStamp);

			_currentFlightTime += Time.deltaTime;

			_rigidbody.transform.position = newPosition;
		}
		else if (!_flightEnded)
		{
			_rigidbody.useGravity = true;
			_rigidbody.isKinematic = false;
			_rigidbody.velocity = (_rigidbody.position - _prevPosition).normalized * 15;
			_flightEnded = true;
		}
	}

	Vector3 prevPoint = default;
	void RotationChangeWhileFlying()
	{
		Vector3 currPoint = transform.position;

		//get the direction (from previous pos to current pos)
		Vector3 currDir = prevPoint - currPoint;

		if (currDir == Vector3.zero)
		{
			return;
		}

		//normalize the direction
		currDir.Normalize();

		//get angle whose tan = y/x, and convert from rads to degrees
		float rotationZ = Mathf.Atan2(currDir.y, currDir.x) * Mathf.Rad2Deg;

		//rotate z based on angle above + an offset (currently 90)
		transform.rotation = Quaternion.Euler(0, 0, rotationZ + 0);

		//store the current point as the old point for the next frame
		prevPoint = currPoint;
	}

	private void OnCollisionEnter(Collision collision)
	{
		Disable(collision.transform, collision.GetContact(0).point);

		if (collision.gameObject.GetComponent<Rigidbody>() != null && collision.gameObject.GetComponent<Rigidbody>() is Rigidbody enemyRigidbody)
		{
			enemyRigidbody.AddForceAtPosition(_rigidbody.velocity, collision.contacts[0].point);
		}
	}

	internal void Disable(Transform hitTransform, Vector3 impactPoint = default)
	{
		Vector3 scale = transform.localScale;
		Vector3 rotation = transform.eulerAngles;

		transform.parent = hitTransform;

		transform.localScale = scale;
		transform.eulerAngles = rotation;
		transform.position = impactPoint;
		
		GetComponentInChildren<Collider>().enabled = false;
		Destroy(this);
		Destroy(_rigidbody);
		_dead = true;
	}
}
