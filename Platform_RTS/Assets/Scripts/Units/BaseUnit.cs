﻿using System.Collections;
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

	public BaseWeapon weapoon = default;

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

	protected abstract bool IsCloseToEnemy();

	protected abstract void Move();

	protected abstract void Attack();

	public abstract void Damage(int amount);
}
