using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour {

	private Light lightComponent;

	[Header("Light Base")]
	public bool enableLight;
	public float lightEnableTime;
	private float currentEnableTime = 0.0f;

	[Header("Light Flicker")]
	public bool enableFlicker;
	public float flickerRate;
	private float currentFlickerStrength;
	private float targetFlickerStrength;
	private bool getNewTargetStrength = true;

	[Header("Light Range")]
	public float lightRangeMin;
	public float lightRangeMax;

	[Header("Light Colour")]
	public Color lightColourMin;
	public Color lightColourMax;

	[Header("Light Intensity")]
	public float lightIntensityMin;
	public float lightIntensityMax;


	// Use this for initialization
	void Start () 
	{
		lightComponent = GetComponent<Light> ();	
		currentFlickerStrength = Random.Range (0.0f, 1.0f);
		SetLightParameters ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (enableLight)
		{
			if (currentEnableTime < lightEnableTime)
			{
				currentEnableTime = Mathf.Clamp (currentEnableTime + Time.deltaTime, 0.0f, lightEnableTime);
				ToggleLight ();
			} else
			{
				if (getNewTargetStrength)
				{
					targetFlickerStrength = Random.Range (0.0f, 1.0f);
					getNewTargetStrength = false;
				} else
				{
					if (targetFlickerStrength > currentFlickerStrength)
					{
						currentFlickerStrength = Mathf.Clamp (currentFlickerStrength + (Time.deltaTime * flickerRate), currentFlickerStrength, targetFlickerStrength);
						SetLightParameters ();

					} else if (targetFlickerStrength < currentFlickerStrength)
					{
						currentFlickerStrength = Mathf.Clamp (currentFlickerStrength - (Time.deltaTime * flickerRate), targetFlickerStrength, currentFlickerStrength);
						SetLightParameters ();
					} else
					{
						getNewTargetStrength = true;
					}
				}
			}

		} else
		{
			currentEnableTime = Mathf.Clamp (currentEnableTime - Time.deltaTime, 0.0f, lightEnableTime);
			ToggleLight ();
		}
	}

	void SetLightParameters()
	{
		if (lightComponent)
		{
			lightComponent.range = Mathf.Lerp (lightRangeMin, lightRangeMax, GetFlickerAmountNormalised());
			lightComponent.color = Color.Lerp (lightColourMin, lightColourMax, GetFlickerAmountNormalised ());
			lightComponent.intensity = Mathf.Lerp (lightIntensityMin, lightIntensityMax, GetFlickerAmountNormalised ());
		}
	}

	void ToggleLight()
	{
		if (lightComponent)
		{
			lightComponent.range = Mathf.Lerp (0.0f, Mathf.Lerp (lightRangeMin, lightRangeMax, GetFlickerAmountNormalised()), GetDisableTimeNormalised());
			lightComponent.intensity = Mathf.Lerp (0.0f, Mathf.Lerp (lightIntensityMin, lightIntensityMax, GetFlickerAmountNormalised ()), GetDisableTimeNormalised());
		}
	}

	float GetFlickerAmountNormalised()
	{
		return currentFlickerStrength / 1.0f;
	}

	float GetDisableTimeNormalised()
	{
		return currentEnableTime / lightEnableTime;
	}
}
