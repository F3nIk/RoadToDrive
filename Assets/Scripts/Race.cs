using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Race : MonoBehaviour
{
	public static UnityAction RaceStarted;
    public static bool IsStart = false;
	public static bool IsContinue = false;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject raceMenu;
	[SerializeField] private ImageFiller adTimerBar;
	[SerializeField] private GameObject refillPanel;
	[SerializeField] private float adTimer;
	private bool isEnded = false;

	private bool isAdWatchToContinue = false;

	private void OnEnable()
	{
		//PlayerVehicle.RideEnded += OnRideEnded;
	}

	private void OnDisable()
	{
		//PlayerVehicle.RideEnded -= OnRideEnded;
	}

	public void StartRace()
    {
		ChangePhase();
		RaceStarted?.Invoke();

	}

	private void OnRideEnded()
	{
		if (isEnded == false)
		{
			StartCoroutine(EndTimer());
		}
	}

	private IEnumerator EndTimer()
	{
		isEnded = true;

		if (isAdWatchToContinue == false)
		{
			AdToContinue();
		}

		float timer = adTimer;
		while (timer >= 0)
		{
			timer -= Time.deltaTime / timer;

			if (IsContinue)
			{
				AdToContinue();
				IsContinue = false;
				isEnded = false;
				isAdWatchToContinue = true;
				StopAllCoroutines();
			}

			yield return null;
		}
		if (IsContinue == false)
		{
			isEnded = false;
			ChangePhase();
		}
	}

	private void AdToContinue()
	{
		refillPanel.SetActive(!refillPanel.activeInHierarchy);

		if (refillPanel.activeInHierarchy)
		{
			adTimerBar.SetTimer(adTimer);
		}
	}

	private void ChangePhase()
	{
		IsStart = !IsStart;
		mainMenu.SetActive(!mainMenu.activeInHierarchy);
		raceMenu.SetActive(!raceMenu.activeInHierarchy);

		refillPanel.SetActive(false);
	}
}   
