using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseManager : MonoBehaviour {

	private CanvasGroup pauseCanvas;
	private bool gamePaused;

	// Use this for initialization
	void Start () 
	{
		pauseCanvas = GetComponent<CanvasGroup> ();

		if (pauseCanvas)
		{
			pauseCanvas.alpha = 0.0f;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		print ("Updating");

		if(Input.GetButtonDown("Pause"))
		{
			Pause ();
		}			
	}

	public void Pause()
	{
		gamePaused = !gamePaused;
		Time.timeScale = gamePaused ? 0.0f : 1.0f;
		pauseCanvas.alpha = gamePaused ? 1.0f : 0.0f;
	}
}
