using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

	public static AudioManager instance = null;

	[Header("Mixers")]
	public AudioMixer masterMixer;

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

	public void SetMixerGroupVolume(string parameter, float value)
	{
		masterMixer.SetFloat (parameter, value);
	}
}
