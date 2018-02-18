using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsScreen : MonoBehaviour {

	[Header("Managers")]
	public UIManager uiManager;
	public AudioManager audioManager;

	//TODO: Sliders need updating on start, accessed from variables.

	void Start()
	{
		if (uiManager == null)
		{
			uiManager = UIManager.instance;
		}

		if (audioManager == null)
		{
			audioManager = AudioManager.instance;
		}
	}

	public void SetMasterVolume(float newVolume)
	{
		audioManager.SetMixerGroupVolume ("masterVolume", Mathf.Lerp(-80.0f, 0.0f, newVolume));
	}

	public void SetMusicVolume(float newVolume)
	{
		audioManager.SetMixerGroupVolume ("musicVolume", Mathf.Lerp(-80.0f, 0.0f, newVolume));
	}

	public void SetAmbienceVolume(float newVolume)
	{
		audioManager.SetMixerGroupVolume ("ambienceVolume", Mathf.Lerp(-80.0f, 0.0f, newVolume));
	}

	public void SetEffectsVolume(float newVolume)
	{
		audioManager.SetMixerGroupVolume ("effectsVolume", Mathf.Lerp(-80.0f, 0.0f, newVolume));
	}
}
