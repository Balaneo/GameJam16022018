using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public enum GameStateEnum
	{
		Menu,
		Loading,
		Initialised,
		BeforeTimer,
		DuringTimer,
		AfterTimer,
		PostGame
	};

	GameStateEnum gameState;

	// Use this for initialization
	void Start () 
	{
		print ("I am the game manager!");
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
