﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseManager : MonoBehaviour {

	public bool gamePaused;

	public void SetPause(bool paused)
	{
		gamePaused = paused;
		Time.timeScale = gamePaused ? 0.0f : 1.0f;
	}

	public bool isPaused()
	{
		print (gamePaused);
		return gamePaused;
	}
}
