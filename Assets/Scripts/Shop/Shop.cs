using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private PlayerVehicle vehicle => player.GetComponent<PlayerVehicle>();
    private PlayerWallet wallet => player.GetComponent<PlayerWallet>();

    [Space(10)]
    [SerializeField] private TextMeshProUGUI maxSpeedLabel;
    [SerializeField] private TextMeshProUGUI accelerationLabel;
    [SerializeField] private TextMeshProUGUI mobilityLabel;
    [SerializeField] private TextMeshProUGUI maxFuelLabel;
    [SerializeField] private TextMeshProUGUI fuelConsumptionLabel;

    private int maxSpeedLevel;
    private int accelerationLevel;
    private int mobilityLevel;
    private int maxFuelLevel;
    private int fuelConsumptionLevel;
    private float averageLevel => (maxSpeedLevel + accelerationLevel + mobilityLevel + maxFuelLevel + fuelConsumptionLevel) / 5;
    private int baseCost = 10;

    private const float MAX_SPEED_ADD = 1.5f;
    private const float ACCELERATION_ADD = 0.005f;
    private const float MOBILITY_ADD = 0.15f;
    private const float MAX_FUEL_ADD = 1.5f;
    private const float FUEL_CONSUMPTION_REDUCE_ADD = 0.05f;

    private void Awake()
	{
        Initialize();
	}

	private void OnApplicationFocus(bool focus)
	{
        SaveData();
	}

	private void OnApplicationPause(bool pause)
	{
        SaveData();
    }

	private void OnApplicationQuit()
	{
        SaveData();
    }

	private void Initialize()
    {
        if(PlayerPrefs.HasKey("Shop"))
        {
            maxSpeedLevel = PlayerPrefs.GetInt("MaxSpeedLevel");
            accelerationLevel = PlayerPrefs.GetInt("AccelerationLevel");
            mobilityLevel = PlayerPrefs.GetInt("MobilityLevel");
            maxFuelLevel = PlayerPrefs.GetInt("MaxFuelLevel");
            fuelConsumptionLevel = PlayerPrefs.GetInt("FuelConsumptionLevel");
        }
        else
        {
            maxSpeedLevel = 1;
            accelerationLevel = 1;
            mobilityLevel = 1;
            maxFuelLevel = 1;
            fuelConsumptionLevel = 1;
        }
        UpdateLabels();

    }

    private void SaveData()
    {
        if(PlayerPrefs.HasKey("Shop") == false)
        {
            PlayerPrefs.SetInt("Shop", 1);
		}

        PlayerPrefs.SetInt("MaxSpeedLevel", maxSpeedLevel);
        PlayerPrefs.SetInt("AccelerationLevel", accelerationLevel);
        PlayerPrefs.SetInt("MobilityLevel", mobilityLevel);
        PlayerPrefs.SetInt("MaxFuelLevel", maxFuelLevel);
        PlayerPrefs.SetInt("FuelConsumptionLevel", fuelConsumptionLevel);
    }

    public void MaxSpeedUpgrade()
    {
        var cost = Mathf.CeilToInt((baseCost * maxSpeedLevel + baseCost * averageLevel));
        if (cost <= wallet.Cash)
        {
            wallet.Consume(cost);
            vehicle.MaxSpeedUp(MAX_SPEED_ADD);
            maxSpeedLevel++;
            UpdateLabels();
            AudioManager.Instance.ShopSamplePlay();
        }
	}

    public void AccelerationUpgrade()
    {
        var cost = Mathf.CeilToInt((baseCost * accelerationLevel + baseCost * averageLevel));
        if (cost <= wallet.Cash)
        {
            wallet.Consume(cost);
            vehicle.AccelerationUp(ACCELERATION_ADD);
            accelerationLevel++;
            UpdateLabels();
            AudioManager.Instance.ShopSamplePlay();
        }
    }

    public void MobilityUpgrade()
    {
        var cost = Mathf.CeilToInt((baseCost * mobilityLevel + baseCost * averageLevel));
        if (cost <= wallet.Cash)
        {
            wallet.Consume(cost);
            vehicle.MobilityUp(MOBILITY_ADD);
            mobilityLevel++;
            UpdateLabels();
            AudioManager.Instance.ShopSamplePlay();
        }
    }

    public void MaxFuelUpgrade()
    {
        var cost = Mathf.CeilToInt((baseCost * maxFuelLevel + baseCost * averageLevel));
        if (cost <= wallet.Cash)
        {
            wallet.Consume(cost);
            vehicle.MaxFuelUp(MAX_FUEL_ADD);
            maxFuelLevel++;
            UpdateLabels();
            AudioManager.Instance.ShopSamplePlay();
        }
    }

    public void FuelConsumptionUpgrade()
    {
        var cost = Mathf.CeilToInt((baseCost * fuelConsumptionLevel + baseCost * averageLevel));
        if (cost <= wallet.Cash)
        {
            wallet.Consume(cost);
            vehicle.FuelConsumptionUp(FUEL_CONSUMPTION_REDUCE_ADD);
            fuelConsumptionLevel++;
            UpdateLabels();
            AudioManager.Instance.ShopSamplePlay();
        }
    }

    private void UpdateLabels()
    {
        maxSpeedLabel.text = "$" + ((int)(baseCost * maxSpeedLevel + baseCost * averageLevel)).ToString();
        accelerationLabel.text = "$" + ((int)(baseCost * accelerationLevel + baseCost * averageLevel)).ToString();
        mobilityLabel.text = "$" + ((int)(baseCost * mobilityLevel + baseCost * averageLevel)).ToString();
        maxFuelLabel.text = "$" + ((int)(baseCost * maxFuelLevel + baseCost * averageLevel)).ToString();
        fuelConsumptionLabel.text = "$" + ((int)(baseCost * fuelConsumptionLevel + baseCost * averageLevel)).ToString();
    }
}
