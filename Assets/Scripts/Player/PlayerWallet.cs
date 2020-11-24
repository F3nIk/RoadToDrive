using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerWallet : IAdsReward
{
	[SerializeField] private TextMeshProUGUI label;
	[SerializeField] private int cashReward;
    [SerializeField] public int Cash { get; private set; }

	private PlayerVehicle playerVehicle;

	private float cashToLabel;

	private void Awake()
	{
		Initialize();
	}

	private void Start()
	{
		playerVehicle = GetComponent<PlayerVehicle>();
		cashToLabel = 0;
	}

	private void Update()
	{
		if(label.gameObject.activeInHierarchy)
		{
			cashToLabel = Mathf.Lerp(cashToLabel, Cash, Time.deltaTime * 2.5f);
			if(Cash - cashToLabel < 1 && Cash - cashToLabel > 0)
			{
				cashToLabel = Cash;
			}
			label.text = "$" + ((int)(cashToLabel)).ToString();
		}
	}

	private void OnApplicationPause(bool pause)
	{
		SaveData();	
	}

	private void OnApplicationFocus(bool focus)
	{
		SaveData();

	}

	private void OnApplicationQuit()
	{
		SaveData();

	}

	public void AddCash(int amount)
	{
		if (playerVehicle.moveType == MoveType.Race)
		{
			Cash += amount;
		}
	}

	public void Consume(int amount)
	{
		Cash -= amount;
	}

	public override void GetAdReward(string rewardId)
	{
		if (rewardId == "Cash")
		{
			Cash += GetRandomRewardValue();
		}
	}

	private int GetRandomRewardValue()
	{
		var random = Random.value;

		if (random <= 1 && random > 0.5f)
		{
			return 100;
		}
		else if (random <= 0.5f && random > 0.25f)
		{
			return 250;
		}
		else if (random <= 0.25f && random > 0.125f)
		{
			return 500;
		}
		else if (random <= 0.125f && random > 0.0625f)
		{
			return 1000;
		}
		else if (random <= 0.0625f && random > 0)
		{
			return 2500;
		}
		else return 50;
	}

	private void Initialize()
	{
		if(PlayerPrefs.HasKey("Wallet"))
		{
			Cash = PlayerPrefs.GetInt("Cash");
		}
		else
		{
			Cash = 0;
			SaveData();
		}
	}

	private void SaveData()
	{
		if(PlayerPrefs.HasKey("Wallet") == false)
		{
			PlayerPrefs.SetInt("Wallet", 1);
		}

		PlayerPrefs.SetInt("Cash", Cash);
	}
}
