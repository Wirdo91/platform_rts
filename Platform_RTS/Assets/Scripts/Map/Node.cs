using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
	public List<Connection> _connectedNodes = default;

	public Vector3 position => transform.position;

	private void Start()
	{
		foreach (Node node in FindObjectsOfType<Node>())
		{
			if (node.position == position && node != this)
			{
				_connectedNodes.Add(new Connection() { node = node, open = true });
			}
		}
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
public class Connection
{
	public Node node = default;
	public bool open = true;
}
