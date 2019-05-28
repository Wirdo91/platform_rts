using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PathFinder
{
	private static List<PathNode> _openNodes = new List<PathNode>();
	private static List<PathNode> _closedNodes = new List<PathNode>();

	public static List<Node> GetPath(Node currentNode, Node endNode)
	{
		_openNodes = new List<PathNode>();
		_closedNodes = new List<PathNode>();

		_openNodes.Add(new PathNode() { node = currentNode });

		while (_openNodes.Count > 0)
		{
			PathNode pathNode = _openNodes[0];
			_openNodes.Remove(pathNode);

			//End found
			if (pathNode.node == endNode)
			{
				PathNode currentPathNode = pathNode;
				List<Node> resultPath = new List<Node>();

				while (currentPathNode.parent != null)
				{
					resultPath.Add(currentPathNode.node);
					currentPathNode = currentPathNode.parent;
				}

				return resultPath;
			}
			else
			{
				foreach (Connection connection in pathNode.node.connectedNodes)
				{
					if (_closedNodes.Contains(new PathNode() { node = connection.node}))
					{
						continue;
					}

					if (connection.open)
					{
						_openNodes.Add(new PathNode() { node = connection.node, parent = pathNode });
					}
				}
				_openNodes.OrderBy((PathNode pn) => Vector3.Distance(endNode.position, pn.node.position));
			}

			_closedNodes.Add(pathNode);
		}

		return new List<Node>() { currentNode };
	}

	private class PathNode
	{
		public Node node;
		public PathNode parent;

		public override bool Equals(object obj)
		{
			return (obj is PathNode) && (obj as PathNode).node == node;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
