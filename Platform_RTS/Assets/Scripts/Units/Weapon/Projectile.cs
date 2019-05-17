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
	private float _initialForce = default;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	public void Init(float force)
	{
		_initialForce = force;

		Debug.DrawLine(transform.position, transform.position + (transform.right * -5), Color.blue, 5);

		_rigidbody.AddForce(transform.right * -_initialForce, ForceMode.Impulse);
	}

	private void OnCollisionEnter(Collision collision)
	{
		Disable(collision.transform);
	}

	internal void Disable(Transform hitTransform)
	{
		transform.parent = hitTransform;
		GetComponentInChildren<Collider>().enabled = false;
		Destroy(this);
		Destroy(_rigidbody);
	}
}
