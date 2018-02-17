using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public static GameController instance = null;

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

	[Header("Managers")]
	public UIManager uiManager = UIManager.instance;

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
	public void SetCurrentGameState(GameStateEnum newGameState)
	{
		gameState = newGameState;
	}

	public bool HasInitialised()
	{
		return (GetCurrentGameState () >= GameStateEnum.Initialised);
	}
}
