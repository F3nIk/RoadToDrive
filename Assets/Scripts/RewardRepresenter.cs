using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RewardRepresenter : MonoBehaviour
{
    private TextMeshProUGUI label;

	private int value;
	private float mult;
	private float curValue = 0;
	private float curMult = 0;
	private bool isLerp = false;
	


	private void OnDisable()
	{
		isLerp = false;
		Reset();
	}

	private void Start()
	{
		label = GetComponent<TextMeshProUGUI>();
	}

	private void Update()
	{
		if (isLerp)
		{

			curValue = Mathf.Lerp(curValue, value, Time.deltaTime * 2.5f);
			curMult = Mathf.Lerp(curMult, mult, Time.deltaTime * 5f);

			label.text = (Mathf.Ceil(curValue)).ToString() + " x " + System.Math.Round(curMult, 2).ToString();
		}
	}

	public void Replay()
	{
		isLerp = true;
		value = 0;
		mult = 0;
	}

	public void Replay(int reward, float multiplier)
	{
		isLerp = true;
		value = reward;
		mult = multiplier;
		Reset();
	}

	public void SetValues(int reward, float multiplier)
    {
		isLerp = true;
		value = reward;
		mult = multiplier;
		 
	}

	private void Reset()
	{
		curValue = 0;
		curMult = 0;
	}
}
