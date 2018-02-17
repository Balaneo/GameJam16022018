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
	public Canvas exitGame;
	public Canvas hudCanvas;

	public enum UIScreensEnum
	{
		None,
		MainMenu,
		Options,
		Paused,
		Loading,
		ExitGame,
	};

	[Header("Runtime Screens")]
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
			if(pauseCanvas.GetComponent<PauseManager>().isPaused())
			{
				Unpause();
			} else
			{
				Pause();
			}
		}

		if(Input.GetButtonDown("Cancel"))
		{
			Cancel ();
		}		
	}

	private void SetScreenEnabled(Canvas canvas, bool enabled)
	{
		canvas.GetComponent<CanvasGroup> ().alpha = enabled ? 1 : 0;
		canvas.GetComponent<CanvasGroup> ().interactable = enabled;
		canvas.GetComponent<CanvasGroup> ().blocksRaycasts = enabled;
	}

	public void SwitchScreen (UIScreensEnum oldScreen, UIScreensEnum newScreen)
	{



		//Hide old screen
		switch (oldScreen)
		{
		case UIScreensEnum.None:

			previousScreen = UIScreensEnum.None;
			break;

		case UIScreensEnum.MainMenu:

			previousScreen = UIScreensEnum.MainMenu;
			SetScreenEnabled (mainMenuCanvas, false);
			break;

		case UIScreensEnum.Options:

			previousScreen = UIScreensEnum.Options;
			SetScreenEnabled (optionsCanvas, false);
			break;

		case UIScreensEnum.Paused:

			print ("Hiding Pause Screen");
			previousScreen = UIScreensEnum.Paused;
			SetScreenEnabled (pauseCanvas, false);
			break;

		case UIScreensEnum.Loading:

			previousScreen = UIScreensEnum.Loading;
			SetScreenEnabled (loadingCanvas, false);
			break;

		case UIScreensEnum.ExitGame:

			previousScreen = UIScreensEnum.ExitGame;
			SetScreenEnabled (exitGame, false);
			break;

		default:

			print ("Old Screen Defaulted with case: " + oldScreen);
			break;
		}


		//Show new screen
		switch (newScreen)
		{
		case UIScreensEnum.None:

			currentScreen = UIScreensEnum.None;
			break;

		case UIScreensEnum.MainMenu:

			currentScreen = UIScreensEnum.MainMenu;
			SetScreenEnabled (mainMenuCanvas, true);
			break;

		case UIScreensEnum.Options:

			currentScreen = UIScreensEnum.Options;
			SetScreenEnabled (optionsCanvas, true);
			break;

		case UIScreensEnum.Paused:

			print ("Showing Pause Screen");
			currentScreen = UIScreensEnum.Paused;
			SetScreenEnabled (pauseCanvas, true);
			break;

		case UIScreensEnum.Loading:

			currentScreen = UIScreensEnum.Loading;
			SetScreenEnabled (loadingCanvas, true);
			break;

		case UIScreensEnum.ExitGame:

			currentScreen = UIScreensEnum.ExitGame;
			SetScreenEnabled (exitGame, true);
			break;

		default:

			print ("New Screen Defaulted with case: " + newScreen);
			break;
		}
	}
		
	void TriggerInitialFade()
	{
		if (fadeTransitionCanvas)
		{
			fadeTransitionCanvas.GetComponent<FadeTransition> ().StopFade();
			WaitingForInitialFade = false;
		}
	}

	public void ToggleFade(bool fade)
	{
		if (fadeTransitionCanvas)
		{
			if (fade)
			{
				fadeTransitionCanvas.GetComponent<FadeTransition> ().StartFade ();
			} else
			{
				fadeTransitionCanvas.GetComponent<FadeTransition> ().StopFade ();
			}
		}
	}

	public void RemoveLoadingScreen()
	{
		SwitchScreen (currentScreen, UIScreensEnum.None);
	}

	public void StartGame()
	{
		loadingCanvas.GetComponent<LoadingScreen> ().LoadNewScene (1);
		SwitchScreen (currentScreen, UIScreensEnum.Loading);
	}

	public void Options()
	{
		SwitchScreen (currentScreen, UIScreensEnum.Options);
	}

	public void Pause()
	{
		pauseCanvas.GetComponent<PauseManager> ().SetPause (true);
		SwitchScreen (currentScreen, UIScreensEnum.Paused);
	}

	public void Unpause()
	{
		pauseCanvas.GetComponent<PauseManager> ().SetPause (false);
		SwitchScreen (currentScreen, UIScreensEnum.None);
	}

	public void Cancel()
	{
		if (currentScreen == UIScreensEnum.Paused)
		{
			Unpause ();
		} else
		{
			SwitchScreen (currentScreen, previousScreen);
		}
	}

	public void PromptExitGame ()
	{
		SwitchScreen (currentScreen, UIScreensEnum.ExitGame);		
	}

	public void ExitGame()
	{
		Application.Quit ();
	}		
}
