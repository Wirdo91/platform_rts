using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
	[SerializeField] private List<Connection> _connectedNodes = default;

	[SerializeField] private ToggleConnection _toggleConnection = default;

	private float _speedToControl = 1f / 5f;

	public Vector3 position => transform.position;
	public List<Connection> connectedNodes => _connectedNodes;

	[SerializeField] private Image _button = default;

	[SerializeField] private float _controllingTeam = 0;

	private int _team1Count = 0;
	private int _team2Count = 0;

	private void Awake()
	{
		foreach (Node node in FindObjectsOfType<Node>())
		{
			if (node.position == position && node != this)
			{
				_connectedNodes.Add(new Connection() { node = node, open = true });
			}
		}

		_button = GetComponentInChildren<Image>();
	}

	private void Update()
	{
		if (_team1Count > 0 && _team2Count <= 0)
		{
			_controllingTeam -= _speedToControl * Time.deltaTime;
		}
		else if (_team2Count > 0 && _team1Count <= 0)
		{
			_controllingTeam += _speedToControl * Time.deltaTime;
		}

		_controllingTeam = Mathf.Clamp(_controllingTeam, -1, 1);
		if (_button != null)
		{
			_button.color = _controllingTeam < 0 ? Color.Lerp(Color.white, Color.red, _controllingTeam * -1) : Color.Lerp(Color.white, Color.green, _controllingTeam);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponentInParent<BaseUnit>() is BaseUnit baseUnit)
		{
			switch (baseUnit.team)
			{
				case BaseUnit.Team.Team1:
					_team1Count++;
					break;
				case BaseUnit.Team.Team2:
					_team2Count++;
					break;
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.GetComponentInParent<BaseUnit>() is BaseUnit baseUnit)
		{
			switch (baseUnit.team)
			{
				case BaseUnit.Team.Team1:
					_team1Count--;
					break;
				case BaseUnit.Team.Team2:
					_team2Count--;
					break;
			}
		}
	}

	private void Toggle()
	{
		_toggleConnection?.Toggle();
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawCube(position, Vector3.one * 0.3f);
		foreach (Connection connection in _connectedNodes)
		{
			if (connection.node)
			{
				Gizmos.color = connection.open ? Color.green : Color.red;
				Gizmos.DrawLine(position, connection.node.position);
			}
		}
	}
}

[Serializable]
public class ToggleConnection
{
	public Transform _bridgeAnchor = default;

	public Connection top = default;
	public Connection bottom = default;

	public float topAngle = default;
	public float bottomAngle = default;

	private bool currentlyAtTop = false;

	public void Toggle()
	{
		if (currentlyAtTop)
		{
			_bridgeAnchor.SetLocalZRotation(topAngle);
		}
		else
		{
			_bridgeAnchor.SetLocalZRotation(bottomAngle);
		}

		currentlyAtTop = !currentlyAtTop;
	}
}

[Serializable]
public class Connection
{
	public Node node = default;
	public bool open = true;
}
