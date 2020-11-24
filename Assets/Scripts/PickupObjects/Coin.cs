using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
	[SerializeField] private int amount;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		RewardCollector.AddCash(amount);
		Destroy(gameObject);
	}
}
