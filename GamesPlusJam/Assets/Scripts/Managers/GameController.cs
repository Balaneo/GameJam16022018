using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public static GameController instance = null;

	public delegate void GameStateChanged(GameStateEnum newState);
	public static event GameStateChanged  OnGameStateChanged;

	public enum GameStateEnum
	{
		None,
		Menu,
		Loading,
		Initialised,
		BeforeTimer,
		DuringTimer,
		AfterTimer,
		PostGame
	};

	public GameStateEnum gameState;

	[Header("Managers")]
	public UIManager uiManager;
	public AudioManager audioManager;

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
		if (uiManager == null)
		{
			uiManager = UIManager.instance;
		}

		if (audioManager == null)
		{
			audioManager = AudioManager.instance;
		}

		SetCurrentGameState (GameStateEnum.Menu);

		SceneManager.sceneLoaded += OnNewScene;

		audioManager.StartMusic ();
	}

	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnNewScene;
	}

	void OnNewScene(Scene scene, LoadSceneMode mode)
	{
		switch (scene.buildIndex)
		{

		case 0:
			SetCurrentGameState (GameStateEnum.Menu);
			break;

		default:
			SetCurrentGameState (GameStateEnum.Initialised);
			break;
		}			
	}

	// Returns the current game state.
	public GameStateEnum GetCurrentGameState()
	{
		return gameState;
	}

	// Sets the current game state, returns whether it actually changed.
	public void SetCurrentGameState(GameStateEnum newGameState)
	{
		gameState = newGameState;

		if (OnGameStateChanged != null)
		{
			OnGameStateChanged (newGameState);
		}
	}

	public bool HasInitialised()
	{
		return (GetCurrentGameState () >= GameStateEnum.Initialised);
	}
}
