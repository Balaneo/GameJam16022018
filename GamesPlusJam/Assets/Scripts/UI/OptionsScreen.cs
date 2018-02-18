using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsScreen : MonoBehaviour {

	[Header("Managers")]
	public UIManager uiManager;
	public AudioManager audioManager;

	[Header("Audio Sliders")]
	public Slider masterVolumeSlider;
	public Slider musicVolumeSlider;
	public Slider ambienceVolumeSlider;
	public Slider effectsVolumeSlider;

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

		UpdateAudioSettings ();
	}

	public void UpdateAudioSettings ()
	{
		SetMasterVolume (audioManager.masterVolume);
		SetMusicVolume (audioManager.musicVolume);
		SetAmbienceVolume (audioManager.ambienceVolume);
		SetEffectsVolume (audioManager.effectsVolume);
	}


	public void SetMasterVolume(float newValue)
	{
		audioManager.SetMasterVolume (newValue);
		masterVolumeSlider.value = newValue;
	}

	public void SetMusicVolume(float newValue)
	{
		audioManager.SetMusicVolume (newValue);
		musicVolumeSlider.value = newValue;
	}

	public void SetAmbienceVolume(float newValue)
	{
		audioManager.SetAmbienceVolume (newValue);
		ambienceVolumeSlider.value = newValue;
	}

	public void SetEffectsVolume(float newValue)
	{
		audioManager.SetEffectsVolume (newValue);
		effectsVolumeSlider.value = newValue;
	}
}
