using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float horizontalSpeed;
	private PlayerVehicle vehicle;
	private Vector3 position;

	private bool isMoved = false;
	private bool isPreviousMoveEnd = true;

	private void OnEnable()
	{
		SwipeDetector.OnSwipe += OnSwipe;
	}

	private void OnDisable()
	{
		SwipeDetector.OnSwipe -= OnSwipe;
	}

	private void Start()
	{
		position = transform.position;
		vehicle = GetComponent<PlayerVehicle>();
	}

	private void Update()
	{
		if (isMoved)
		{
			
			Vector3 newPos = new Vector3(Mathf.Lerp(transform.position.x, position.x, Time.deltaTime * vehicle.mobility), transform.position.y);
			transform.position = newPos;

			var Xdistance = transform.position.x - position.x;

			if(Xdistance < 0.1f && Xdistance > -0.1f)
			{
				newPos = new Vector3(position.x, transform.position.y);
				transform.position = newPos;
				isPreviousMoveEnd = true;
			}
		}
	}

	private void OnSwipe(SwipeData swipeData)
	{
		isMoved = true;

		if (isPreviousMoveEnd)
		{
			isPreviousMoveEnd = false;

			if (swipeData.Direction == SwipeDirection.Left)
			{
				MoveLeft();
			}
			else if (swipeData.Direction == SwipeDirection.Right)
			{
				MoveRight();
			}
		}
	}

	private void MoveLeft()
	{
		if (transform.position.x > -3)
		{
			position = new Vector3(transform.position.x - 3f, transform.position.y);
		}
	}

	private void MoveRight()
	{
		if (transform.position.x < 3)
		{
			position = new Vector3(transform.position.x + 3f, transform.position.y);
		}
	}
}
