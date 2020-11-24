using GooglePlayGames.BasicApi;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public enum MoveType
{
	Preview,
	Race
}

public class PlayerVehicle : IAdsReward
{
	[SerializeField] private TextMeshProUGUI score;
	[SerializeField] private TextMeshProUGUI bestScore;
	[SerializeField] private ImageFiller fuelIcon;
	//[SerializeField] private TextRepresenter fuelLabel;

	[SerializeField] private TextRepresenter mileageLabel;
	[SerializeField] private ImageFiller imageFuelIcon;
	[SerializeField] private TextRepresenter fuelLabel;

	public float BestMileage { get; private set; }
	[SerializeField] private float acceleration;
	[SerializeField] private float speed;
	[SerializeField] private float maxSpeed;
	[SerializeField] private float fuel;
	[SerializeField] private float maxFuel;
	[SerializeField] private float baseFuelConsumption;
	[SerializeField] private float mileage;
	public float mobility { get; private set; }

	[SerializeField] private bool advanceUI = false;

	public MoveType moveType { get; private set; } = MoveType.Preview;

	private float valueReduce = 0.25f;
	private float previewSpeedReduce = 0.025f;
	private float speedRatio => speed / maxSpeed;
	private float fuelRatio => fuel / maxFuel;
	private float fuelConsumption => baseFuelConsumption * (Mathf.Lerp(1.5f,1f,speedRatio)) * valueReduce;
	private bool isRefilling = false;

	public static UnityAction FuelEnded;

	private void Awake()
	{
		Initialize();
	}

	private void OnEnable()
	{
		GameStateManager.RaceStarted += ChangeMovementType;
		GameStateManager.RaceEnded += ChangeMovementType;
	}

	private void OnDisable()
	{
		GameStateManager.RaceStarted -= ChangeMovementType;
		GameStateManager.RaceEnded -= ChangeMovementType;

	}

	private void Start()
	{
		StartCoroutine(Movement());
		UpdateBestScore();
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

	private void Initialize()
	{
		Debug.Log("player");
		if (PlayerPrefs.HasKey("Vehicle"))
		{
			maxFuel = PlayerPrefs.GetFloat("MaxFuel");
			maxSpeed = PlayerPrefs.GetFloat("MaxSpeed");
			acceleration = PlayerPrefs.GetFloat("Acceleration");
			baseFuelConsumption = PlayerPrefs.GetFloat("Consumption");
			BestMileage = PlayerPrefs.GetFloat("BestScore");
			mobility = PlayerPrefs.GetFloat("Mobility");
		}
		else
		{
			maxFuel = 40;
			maxSpeed = 50;
			acceleration = 0.125f;
			baseFuelConsumption = 10f;
			BestMileage = 0;
			mobility = 5f;
			SaveData();
		}
	}

	private void SaveData()
	{
		if(PlayerPrefs.HasKey("Vehicle") == false)
		{
			PlayerPrefs.SetInt("Vehicle", 1);
		}

		PlayerPrefs.SetFloat("MaxFuel", maxFuel);
		PlayerPrefs.SetFloat("MaxSpeed", maxSpeed);
		PlayerPrefs.SetFloat("Acceleration", acceleration);
		PlayerPrefs.SetFloat("Consumption", baseFuelConsumption);
		PlayerPrefs.SetFloat("BestScore", BestMileage);
		PlayerPrefs.SetFloat("Mobility", mobility);
		PlayerPrefs.Save();

	}

	private void UpdateRaceInfo()
	{
		if (advanceUI)
		{
			imageFuelIcon.UpdateAmount(fuelRatio);
			fuelLabel.RepresentUpdate((int)(fuelRatio * 100) + "%");
			mileageLabel.RepresentUpdate(Math.Round(mileage / 1000, 2) + " km");
		}
		else
		{
			score.text = Math.Round(mileage / 1000, 2) + "km";
			fuelIcon.UpdateAmount(fuelRatio);
		}
	}

	private void UpdateBestScore()
	{
		bestScore.text = "best " + Math.Round(BestMileage / 1000, 2) + "km";
	}

	#region Movement

	private void ChangeMovementType(MoveType type)
	{
		moveType = type;
		if(type == MoveType.Race)
		{
			Refill();
			AudioManager.Instance.EngineSamplePlay();
		}
		if(type == MoveType.Preview)
		{
			UpdateBestScore();
			mileage = 0;
			score.text = Math.Round(mileage / 1000, 2) + "km";
			AudioManager.Instance.EngineSampleStop();
		}
	}

	private IEnumerator Movement()
	{
		while(true)
		{
			float virtualSpeed = 0;

			if(moveType == MoveType.Preview)
			{
				virtualSpeed = GetPreviewSpeed();
			}
			else if(moveType == MoveType.Race)
			{
				virtualSpeed = GetRaceSpeed();
				Mileagemeter(virtualSpeed);
				ChangeSpeed();
				UpdateRaceInfo();
			}

			transform.Translate(Vector2.up * virtualSpeed);
			yield return null;
		}
	}

	private float GetPreviewSpeed()
	{
		return maxSpeed * previewSpeedReduce * Time.deltaTime;
	}

	private float GetRaceSpeed()
	{
		return speed * Time.deltaTime * valueReduce;
		
	} 

	private void ChangeSpeed()
	{
		if(fuelRatio > 0.05f)
		{
			Acceleration();
		}
		else
		{
			Stopping();
		}
	}

	private void Acceleration()
	{
		speed = Mathf.Lerp(speed, maxSpeed, Time.deltaTime * acceleration);
		FuelConsumption();
	}

	private void Stopping()
	{
		if (speed > 0)
		{
			speed -= speed * acceleration * Time.deltaTime + 0.05f;
			FuelConsumption();
		}
		else
		{
			speed = 0;

			if (mileage > BestMileage)
			{
				BestMileage = mileage;
			}

			if (isRefilling == false)
			{
				isRefilling = true;
				FuelEnded?.Invoke();
			}
		}
	}

	private void Mileagemeter(float amount)
	{
		mileage += amount;

		if(mileage % 5000 < 0.5f)RewardCollector.AddMultiplier(mileage / 5000);
	}

	#endregion

	#region Fuel

	public void AddFuel(float amount)
	{
		if (moveType == MoveType.Race)
		{
			fuel = Mathf.Clamp(fuel + amount, 0, maxFuel);
			AudioManager.Instance.PickupGasSamplePlay();
		}
	}

	private void FuelConsumption()
	{
		if (moveType == MoveType.Race)
		{
			if (fuel > 0)
			{
				fuel -= fuelConsumption * Time.deltaTime;
			}
			else
			{
				fuel = 0;
			}
		}
	}

	private void Refill()
	{
		fuel = maxFuel;
		mileage = 0;
		isRefilling = false;
	}

	#endregion

	#region Upgrades

	public void MaxSpeedUp(float amount)
	{
		maxSpeed += amount;	
	}

	public void AccelerationUp(float amount)
	{
		acceleration += amount;
	}
	public void MobilityUp(float amount)
	{
		mobility += amount;
	}
	public void MaxFuelUp(float amount)
	{
		maxFuel += amount;
	}
	public void FuelConsumptionUp(float amount)
	{
		baseFuelConsumption -= amount;
	}
	#endregion

	public override void GetAdReward(string rewardId)
	{
		if (rewardId == "Fuel")
		{
			AddFuel(maxFuel);
			moveType = MoveType.Race;
			isRefilling = false;
		}
	}


}
