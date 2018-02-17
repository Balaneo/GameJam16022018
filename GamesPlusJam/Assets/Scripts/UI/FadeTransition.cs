using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeTransition : MonoBehaviour {

	public bool FadeOn = false;

	private float fadeTimeCurrent;

	public float fadeTimeMax = 3.0f;

	private CanvasGroup canvasGroup;

	void Start()
	{
		canvasGroup = GetComponent<CanvasGroup> ();

		if (FadeOn)
		{
			fadeTimeCurrent = fadeTimeMax;
		} else
		{
			fadeTimeCurrent = 0.0f;
		}

		SetFadeAmount();
	}

	// Update is called once per frame
	void Update ()
	{
		if (FadeOn)
		{
			if (fadeTimeCurrent < fadeTimeMax)
			{
				fadeTimeCurrent = Mathf.Clamp(fadeTimeCurrent + Time.deltaTime, 0.0f, fadeTimeMax);
				SetFadeAmount();
			}
		} 
		else
		{
			if (fadeTimeCurrent > 0.0f)
			{
				fadeTimeCurrent = Mathf.Clamp(fadeTimeCurrent - Time.deltaTime, 0.0f, fadeTimeMax);
				SetFadeAmount();
			}
		}
	}

	public void StartFade()
	{
		FadeOn = true;
	}

	public void StopFade()
	{
		FadeOn = false;
	}

	private void SetFadeAmount()
	{
		if (canvasGroup)
		{
			canvasGroup.alpha = Mathf.Lerp (0.0f, 1.0f, Mathf.Pow(fadeTimeCurrent / fadeTimeMax, 5.0f));
		}
	}
}
