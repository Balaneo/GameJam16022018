using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Canvas fadeTransitionCanvas;

	public bool WaitingForInitialFade = true;

	void Awake()
	{
		DontDestroyOnLoad (this.gameObject);
	}

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (WaitingForInitialFade)
		{
			if (fadeTransitionCanvas)
			{
				fadeTransitionCanvas.GetComponent<FadeTransition> ().StopFade();
				WaitingForInitialFade = false;
			}
		}		
	}
}
