using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

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

	GameStateEnum gameState = GameStateEnum.None;

	UIManager uiManager;

	void Awake()
	{
		DontDestroyOnLoad (this.gameObject);
	}

	// Use this for initialization
	void Start () 
	{
		uiManager = GameObject.FindGameObjectWithTag ("UIManager").GetComponent<UIManager> ();

		if (uiManager)
		{
			print ("Found UI Manager");
			SetCurrentGameState (GameStateEnum.Menu);
			uiManager.SwitchScreen (UIManager.UIScreensEnum.None, UIManager.UIScreensEnum.MainMenu);
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	// Returns the current game state.
	public GameStateEnum GetCurrentGameState()
	{
		return gameState;
	}

	// Sets the current game state, returns whether it actually changed.
	private bool SetCurrentGameState(GameStateEnum newGameState)
	{
		bool gameStateChanged = false;

		if (newGameState != gameState)
		{
			gameState = newGameState;
			gameStateChanged = true;
		}

		return gameStateChanged;
	}

	public bool HasInitialised()
	{
		return (GetCurrentGameState () >= GameStateEnum.Initialised);
	}
}
