using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ImageFiller : MonoBehaviour
{
    private Image image;

	private bool isTimerSet = false;
	private float timer;

	private void OnDisable()
	{
		image.fillAmount = 1;
	}

	private void Awake()
	{
		image = GetComponent<Image>();
	}

	private void Update()
	{
		if (isTimerSet)
		{
			if (image.fillAmount > 0)
			{
				image.fillAmount -= Time.deltaTime / timer;
			}
			else
			{
				isTimerSet = false;
				
			}
		} 
	}

	public void UpdateAmount(float ratio)
    {
		image.fillAmount = ratio;
	}

	public void SetTimer(float seconds)
	{
		isTimerSet = true;
		timer = seconds;
	}
}
