using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

	[Header("Managers")]
	public GameController gameController;
	public UIManager uiManager;

	[Header("Spawn Point")]
	public int spawnIndex;
	public GameObject spawnedPlayerReference;
	public float spawnHeightOffset = 1.0f;

	[Header("Debug")]
	public bool testing;
	public GameObject playerTestObject;
	public KeyCode testKeycode;


	public void Start()
	{
		if (uiManager == null)
		{
			uiManager = UIManager.instance;
		}

		if (gameController == null)
		{
			gameController = GameController.instance;
			GameController.OnGameStateChanged += CheckShouldSpawnPlayer;

			if (gameController.GetCurrentGameState() == GameController.GameStateEnum.Initialised)
			{
				CheckShouldSpawnPlayer (gameController.GetCurrentGameState());
			}
		}
	}

	void CheckShouldSpawnPlayer(GameController.GameStateEnum gameState)
	{
		if (gameState == GameController.GameStateEnum.Initialised)
		{
			SpawnPlayer (playerTestObject);
		}
	}

	public void SpawnPlayer(GameObject newPlayer)
	{
		if (spawnedPlayerReference == null)
		{
			spawnedPlayerReference = Instantiate (newPlayer, new Vector3 (transform.position.x, transform.position.y + spawnHeightOffset, transform.position.z), transform.rotation);
		} else
		{
			print ("I already have an assigned player!");
		}

		gameController.currentPlayer = spawnedPlayerReference;
		uiManager.currentPlayer = spawnedPlayerReference;
	}

	void Update()
	{
		if (testing)
		{
			if(Input.GetKeyDown(testKeycode))
			{
				SpawnPlayer(playerTestObject);
			}
		}
	}


	void OnDisable()
	{
		GameController.OnGameStateChanged -= CheckShouldSpawnPlayer;

		uiManager.currentPlayer = null;
		gameController.currentPlayer = null;
	}
}
