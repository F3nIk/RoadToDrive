using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    [SerializeField] private PlayerVehicle vehicle;
    [SerializeField] private PlayerWallet wallet;
    [SerializeField] private RewardCollector collector;

	public void GetAdReward(string reward)
	{
		switch(reward)
		{
			case "Fuel":
				vehicle.GetAdReward(reward);
				break;
			case "Multiplier":
				collector.GetAdReward(reward);
				break;
			case "Cash":
				wallet.GetAdReward(reward);
				break;
		}
	}

}
		 