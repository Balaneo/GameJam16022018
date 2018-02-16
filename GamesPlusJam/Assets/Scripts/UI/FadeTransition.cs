using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeTransition : MonoBehaviour {

	public bool FadeOn = false;

	private float fadeTimeCurrent;

	public float fadeTimeMax = 2.0f;

	private Image fadeImage;

	void Start()
	{
		fadeImage = GetComponentInChildren<Image> ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (FadeOn)
		{
			if (fadeTimeCurrent < fadeTimeMax)
			{
				fadeTimeCurrent = Mathf.Clamp(fadeTimeCurrent + Time.deltaTime, 0.0f, fadeTimeMax);
				fadeImage.color = Color.Lerp(new Color(0.0f, 0.0f, 0.0f, 0.0f), new Color (0.0f, 0.0f, 0.0f, 1.0f), fadeTimeCurrent / fadeTimeMax);
			}
		} 
		else
		{
			if (fadeTimeCurrent > 0.0f)
			{
				fadeTimeCurrent = Mathf.Clamp(fadeTimeCurrent - Time.deltaTime, 0.0f, fadeTimeMax);
				fadeImage.color = Color.Lerp (new Color(0.0f, 0.0f, 0.0f, 0.0f), new Color (0.0f, 0.0f, 0.0f, 1.0f), fadeTimeCurrent / fadeTimeMax);
			}
		}
	}

	void StartFade()
	{
		FadeOn = true;
	}

	void StopFade()
	{
		FadeOn = false;
	}
}
