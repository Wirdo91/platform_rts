using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WorldManager
{
	private static Dictionary<BaseUnit.Team, TeamManager> _teams = new Dictionary<BaseUnit.Team, TeamManager>()
	{
		{BaseUnit.Team.Team1, new TeamManager()},
		{BaseUnit.Team.Team2, new TeamManager()}
	};

	public static void AddUnit(BaseUnit baseUnit)
	{
		_teams[baseUnit.team].units.Add(baseUnit);
	}

	public static List<BaseUnit> GetEnemiesInRange(BaseUnit unit)
	{
		List<BaseUnit> enemyUnits = _teams[(BaseUnit.Team)((int)unit.team * -1)].units;

		for (int i = enemyUnits.Count - 1; i < 0; i--)
		{
			if (Vector3.Distance(enemyUnits[i].position, unit.position) > unit.range)
			{
				enemyUnits.RemoveAt(i);
			}
		}

		return enemyUnits;
	}
}
