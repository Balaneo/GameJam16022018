using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	[Header("Game Canvases")]
	public Canvas fadeTransitionCanvas;
	public Canvas mainMenuCanvas;
	public Canvas optionsCanvas;
	public Canvas pauseCanvas;
	public Canvas loadingCanvas;
	public Canvas hudCanvas;

	public enum UIScreensEnum
	{
		None,
		MainMenu,
		Options,
		Paused,
		Loading
	};

	public UIScreensEnum currentScreen = UIScreensEnum.None;
	public UIScreensEnum previousScreen = UIScreensEnum.None;

	GameController gameController;

	public bool WaitingForInitialFade = true;

	void Awake()
	{
		DontDestroyOnLoad (this.gameObject);
	}

	// Use this for initialization
	void Start ()
	{
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

		if (gameController)
		{
			print ("Found Game Controller");
		}		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (WaitingForInitialFade)
		{
			TriggerInitialFade ();
		}		

		if(Input.GetButtonDown("Pause"))
		{
			pauseCanvas.GetComponent<PauseManager> ().SetPause (pauseCanvas.GetComponent<PauseManager> ().isPaused () ? false : true);
			SwitchScreen (	pauseCanvas.GetComponent<PauseManager> ().isPaused () ? currentScreen : UIScreensEnum.Paused,
							pauseCanvas.GetComponent<PauseManager> ().isPaused () ? UIScreensEnum.Paused : previousScreen);
		}

		if(Input.GetButtonDown("Cancel"))
		{
			if (previousScreen != UIScreensEnum.Paused && previousScreen != UIScreensEnum.Loading)
			{
				if (currentScreen == UIScreensEnum.Paused)
				{
					pauseCanvas.GetComponent<PauseManager> ().SetPause (false);
				}
				SwitchScreen (currentScreen, previousScreen);
			}
		}		

		if(Input.GetKeyDown(KeyCode.L))
		{			
			loadingCanvas.GetComponent<LoadingScreen> ().StartLoading ();
			SwitchScreen (currentScreen, UIScreensEnum.Loading);
		}

		if(Input.GetKeyDown(KeyCode.M))
		{			
			SwitchScreen (currentScreen, UIScreensEnum.MainMenu);
		}		
	}

	public void SwitchScreen (UIScreensEnum oldScreen, UIScreensEnum newScreen)
	{
		//Hide old screen
		switch (oldScreen)
		{
		case UIScreensEnum.None:

			break;

		case UIScreensEnum.MainMenu:

			previousScreen = UIScreensEnum.MainMenu;
			mainMenuCanvas.GetComponent<CanvasGroup> ().alpha = 0.0f;
			break;

		case UIScreensEnum.Options:

			previousScreen = UIScreensEnum.Options;
			optionsCanvas.GetComponent<CanvasGroup> ().alpha = 0.0f;
			break;

		case UIScreensEnum.Paused:

			previousScreen = UIScreensEnum.Paused;
			pauseCanvas.GetComponent<CanvasGroup> ().alpha = 0.0f;
			break;

		case UIScreensEnum.Loading:

			previousScreen = UIScreensEnum.Loading;
			loadingCanvas.GetComponent<CanvasGroup> ().alpha = 0.0f;
			break;

		default:

			print ("Old Screen Defaulted with case: " + oldScreen);
			break;
		}


		//Show new screen
		switch (newScreen)
		{
		case UIScreensEnum.None:

			break;

		case UIScreensEnum.MainMenu:

			currentScreen = UIScreensEnum.MainMenu;
			mainMenuCanvas.GetComponent<CanvasGroup> ().alpha = 1.0f;
			break;

		case UIScreensEnum.Options:

			currentScreen = UIScreensEnum.Options;
			optionsCanvas.GetComponent<CanvasGroup> ().alpha = 1.0f;
			break;

		case UIScreensEnum.Paused:

			currentScreen = UIScreensEnum.Paused;
			pauseCanvas.GetComponent<CanvasGroup> ().alpha = 1.0f;
			break;

		case UIScreensEnum.Loading:

			currentScreen = UIScreensEnum.Loading;
			loadingCanvas.GetComponent<CanvasGroup> ().alpha = 1.0f;
			break;

		default:

			print ("New Screen Defaulted with case: " + newScreen);
			break;
		}
	}

	//
	void TriggerInitialFade()
	{
		if (fadeTransitionCanvas)
		{
			fadeTransitionCanvas.GetComponent<FadeTransition> ().StopFade();
			WaitingForInitialFade = false;
		}
	}

	public void ExitGame()
	{
		Application.Quit ();
	}		
}
