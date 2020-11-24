using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
	[SerializeField] private Transform target;

	private float posZ;
	private float offsetY = 4f;
	private float speed = 5f;
	private float smoothValue = 0.25f;
	private float smooth = 0;

	private void Awake()
	{
		posZ = transform.position.z;	
	}

	private void Update()
	{
		Vector3 direction = new Vector3(target.position.x, target.position.y + 4, posZ);
		Vector3 rawPosition = Vector3.Lerp(transform.position, direction, Time.deltaTime * speed);
		transform.position = new Vector3(rawPosition.x,rawPosition.y, posZ);

	}
}
