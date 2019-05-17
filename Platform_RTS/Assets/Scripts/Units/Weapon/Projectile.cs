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
		
	}

	public void Init(float force)
	{
		_initialForce = force;
	}
}
