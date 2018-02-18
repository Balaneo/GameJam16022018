using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

	public static AudioManager instance = null;

	[Header("Mixers")]
	public AudioMixer masterMixer;

	[Header("Sources")]
	public AudioSource musicSource;

	[Header("Volumes")]
	[Range(0.0f, 1.0f)]
	public float masterVolume = 0.99f;
	[Range(0.0f, 1.0f)]
	public float musicVolume = 0.5f;
	[Range(0.0f, 1.0f)]
	public float ambienceVolume = 0.5f;
	[Range(0.0f, 1.0f)]
	public float effectsVolume = 0.75f;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		} else if (instance != this)
		{
			Destroy (gameObject);
		}

		DontDestroyOnLoad (this.gameObject);
	}

	void Start()
	{
		
	}

	public void StartMusic()
	{
		musicSource.Play();
	}

	public void StopMusic()
	{
		musicSource.Stop();
	}


	//Audio Settings
	public void SetMasterVolume(float newVolume)
	{
		masterMixer.SetFloat ("masterVolume", Mathf.Lerp(-80.0f, 0.0f, newVolume));
		masterVolume = newVolume;
	}

	public void SetMusicVolume(float newVolume)
	{
		masterMixer.SetFloat ("musicVolume", Mathf.Lerp(-80.0f, 0.0f, newVolume));
		musicVolume = newVolume;
	}

	public void SetAmbienceVolume(float newVolume)
	{
		masterMixer.SetFloat ("ambienceVolume", Mathf.Lerp(-80.0f, 0.0f, newVolume));
		ambienceVolume = newVolume;
	}

	public void SetEffectsVolume(float newVolume)
	{
		masterMixer.SetFloat ("effectsVolume", Mathf.Lerp(-80.0f, 0.0f, newVolume));
		effectsVolume = newVolume;
	}
}
