using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

	public GameObject gameController;
	public GameObject uiManager;

	void Awake()
	{
		if (GameController.instance == null)
		{
			Instantiate (gameController);
		}

		if (UIManager.instance == null)
		{
			Instantiate (uiManager);
		}
	}

}
