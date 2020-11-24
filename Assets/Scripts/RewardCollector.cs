using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RewardCollector : IAdsReward
{
    public static int Cash => (int)(cash * Multiplier);
    public static float Multiplier => (float)System.Math.Round(multiplier, 2); 
    private static int cash;
    private static float multiplier = 1f;

    [SerializeField] private RewardRepresenter rewardLabel;

	private void Update()
	{
		if(rewardLabel.gameObject.activeInHierarchy)
        {
            rewardLabel.SetValues(cash, Multiplier);
		}
	}

	public static void AddCash(int amount)
    {
        cash += amount;
        AudioManager.Instance.PickupCashSamplePlay();
    }

    public static void AddMultiplier(float value)
    {
        multiplier += value;
	}

    public void GetReward()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerWallet>().AddCash(Cash);
        Reset();
	}



	private void Reset()
	{
        cash = 0;
        multiplier = 1f;
    }

    public override void GetAdReward(string rewardId)
    {
        if (rewardId == "Multiplier")
        {
            multiplier += 2;
            rewardLabel.Replay(cash, multiplier);
        }
    }
}
