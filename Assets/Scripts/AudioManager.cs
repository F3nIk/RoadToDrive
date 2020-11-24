using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[SerializeField] private AudioSource engineSorce;
	[SerializeField] private AudioSource shopSorce;
	[SerializeField] private AudioSource walletSorce;
	[SerializeField] private AudioSource gasSorce;
    public static AudioManager Instance;
	private void Awake()
	{
		if(Instance != null)
		{
			Destroy(this);
		}
		else
		{
			Instance = this;
		}
	}

	public void ShopSamplePlay()
	{
		shopSorce.Play();
	}

	public void PickupCashSamplePlay()
	{
		walletSorce.Play();
	}

	public void PickupGasSamplePlay()
	{
		gasSorce.Play();
	}

	public void EngineSamplePlay()
	{
		engineSorce.Play();
	}

	public void EngineSampleStop()
	{
		engineSorce.Stop();
	}
}
