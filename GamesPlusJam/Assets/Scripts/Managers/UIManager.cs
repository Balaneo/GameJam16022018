using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIManager : MonoBehaviour {

	public static UIManager instance = null;

	[Header("Managers")]
	public GameController gameController = GameController.instance;

	[Header("Player")]
	public GameObject currentPlayer;

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
		HUD
	};

	[Header("Runtime Screens")]
	public UIScreensEnum currentScreen = UIScreensEnum.None;
	public UIScreensEnum previousScreen = UIScreensEnum.None;

	public bool WaitingForInitialFade = true;
	public bool playerHUDActive = false;


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

	// Use this for initialization
	void Start ()
	{
		if (gameController == null)
		{
			gameController = GameController.instance;
		}

		GameController.OnGameStateChanged += CheckIfInitialised;

		ResetAllScreens ();

		SwitchScreen (currentScreen, UIScreensEnum.MainMenu);
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
			if (gameController.GetCurrentGameState () > GameController.GameStateEnum.Initialised)
			{
				if(pauseCanvas.GetComponent<PauseManager>().isPaused())
				{
					Unpause();
				} else
				{
					Pause();
				}
			}
		}

		if(Input.GetButtonDown("Cancel"))
		{
			Cancel ();
		}		
	}

	private void CheckIfInitialised(GameController.GameStateEnum newState)
	{
		if (newState == GameController.GameStateEnum.Initialised)
		{
			LoadingScreen.OnPostLoadDelayFinished += ShowPlayerHUD;
		}
	}

	public void ShowPlayerHUD()
	{
		SwitchScreen (currentScreen, UIScreensEnum.HUD);
		gameController.SetCurrentGameState (GameController.GameStateEnum.BeforeTimer);
	}
		
	private void ResetAllScreens()
	{
		SetScreenEnabled (fadeTransitionCanvas, false);
		SetScreenEnabled (mainMenuCanvas, false);
		SetScreenEnabled (optionsCanvas, false);
		SetScreenEnabled (pauseCanvas, false);
		SetScreenEnabled (loadingCanvas, false);
		SetScreenEnabled (exitGame, false);
		SetScreenEnabled (hudCanvas, false);
	}

	private void SetScreenEnabled(Canvas canvas, bool enabled)
	{
		canvas.enabled = enabled;
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

		case UIScreensEnum.HUD:

			previousScreen = UIScreensEnum.HUD;
			SetScreenEnabled (hudCanvas, false);
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

		case UIScreensEnum.HUD:

			currentScreen = UIScreensEnum.HUD;
			SetScreenEnabled (hudCanvas, true);
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

	public void StartLoadingScreen(int newScene)
	{
		pauseCanvas.GetComponent<PauseManager> ().SetPause (false);
		loadingCanvas.GetComponent<LoadingScreen> ().LoadNewScene (newScene);
		SwitchScreen (currentScreen, UIScreensEnum.Loading);
		gameController.SetCurrentGameState (GameController.GameStateEnum.Loading);

	}

	public void RemoveLoadingScreen()
	{
		SwitchScreen (UIScreensEnum.Loading, UIScreensEnum.None);
	}

	public void MainMenu()
	{
		if (SceneManager.GetSceneByBuildIndex (0).name != SceneManager.GetActiveScene ().name)
		{
			StartLoadingScreen (0);
			SceneManager.sceneLoaded += MenuLoaded;
		} else
		{
			SwitchScreen (currentScreen, UIScreensEnum.MainMenu);
		}
	}

	void MenuLoaded(Scene scene, LoadSceneMode mode)
	{
		print ("Loaded scene: " + scene.name);		
		SceneManager.sceneLoaded -= MenuLoaded;
		LoadingScreen.OnPostLoadDelayFinished += MenuPostLoadFinished;
	}

	void MenuPostLoadFinished()
	{
		SwitchScreen (currentScreen, UIScreensEnum.MainMenu);
	}

	public void StartGame()
	{
		StartLoadingScreen (1);
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
		SwitchScreen (currentScreen, UIScreensEnum.HUD);
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
		#if UNITY_EDITOR
		EditorApplication.isPlaying = false;
		#else
		Application.Quit ();
		#endif
	}		
}
