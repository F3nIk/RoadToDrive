using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRepresenter : MonoBehaviour
{
    [SerializeField] private TextRepresenter bestScore;
    [SerializeField] private TextRepresenter cash;
	[SerializeField] private PlayerVehicle vehicle;
	[SerializeField] private PlayerWallet wallet;

	private void OnEnable()
	{
		UpdateLabels();
	}

	private void Start()
	{
		UpdateLabels();
	}



	private void UpdateLabels()
	{
		bestScore.RepresentUpdate("BEST SCORE:\n" + (Math.Round(vehicle.BestMileage / 1000, 2) + " km"));
		cash.RepresentUpdate(wallet.Cash.ToString());
	}
}
