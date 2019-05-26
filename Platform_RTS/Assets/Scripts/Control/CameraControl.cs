using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraControl : MonoBehaviour
{
	private Vector3 _previousMousePosition = Vector3.zero;
	private Camera _camera = default;

	private void Awake()
	{
		_camera = GetComponent<Camera>();
		_previousMousePosition = Input.mousePosition;
	}

	void Update()
    {
		Vector3 deltaMousePosition = Input.mousePosition - _previousMousePosition;
		deltaMousePosition.Scale(new Vector3(-1, -1, -1));
		float deltaMouseScroll = Input.mouseScrollDelta.y;
		deltaMouseScroll *= -1f;

        if (Input.GetMouseButton(2))
		{
			transform.position += deltaMousePosition;
		}
		_camera.orthographicSize += deltaMouseScroll;

		_previousMousePosition = Input.mousePosition;
    }
}
