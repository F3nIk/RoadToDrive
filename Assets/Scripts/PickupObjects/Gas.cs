using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour
{
	[SerializeField] private float amount;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		collision.gameObject.GetComponent<PlayerVehicle>().AddFuel(amount);
		Destroy(gameObject);
	}

}
