using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

	[Header("Managers")]
	public GameController gameController;

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
			gameController.SetCurrentGameState (GameController.GameStateEnum.BeforeTimer);
		}
	}

	public void SpawnPlayer(GameObject newPlayer)
	{
		if (spawnedPlayerReference == null)
		{
			spawnedPlayerReference = Instantiate (newPlayer, new Vector3 (transform.position.x, transform.position.y + spawnHeightOffset, transform.position.z), transform.rotation);
			print ("Spawned player at spawn point: " + spawnIndex);
		} else
		{
			print ("I already have an assigned player!");
		}
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
}
