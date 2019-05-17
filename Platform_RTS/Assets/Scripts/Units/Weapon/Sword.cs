using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : BaseWeapon
{
	[SerializeField] private Transform _model = default;
	bool attacking = false;

	private float _startSwingAngle = 0;
	private float _swingAngle = 130;
	private float _swingTime = 0.5f;

	private void Awake()
	{
		_model.gameObject.SetActive(false);
	}

	public override void Attack(BaseUnit unit = null)
	{
		if (!attacking)
		{
			attacking = true;
			StartCoroutine(AttackAnimation());
		}
	}

	private IEnumerator AttackAnimation()
	{
		_model.gameObject.SetActive(true);
		transform.SetLocalZRotation(_startSwingAngle);

		float currentSwingAngle = _startSwingAngle;
		float currentSwingTime = 0;

		while (currentSwingTime < _swingTime)
		{
			currentSwingTime += Time.deltaTime;

			currentSwingAngle = Mathf.Lerp(_startSwingAngle, _swingAngle, currentSwingTime / _swingTime);

			transform.SetLocalZRotation(currentSwingAngle);

			yield return null;
		}

		yield return null;

		_model.gameObject.SetActive(false);
		attacking = false;
	}
}
