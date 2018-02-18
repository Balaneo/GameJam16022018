using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour {

	private int newSceneIndex;

	private bool loadingScene;

	[Header("Managers")]
	public UIManager uiManager;

	[Header("Loading")]
	public Text loadingText;

	[Header("Hints")]
	public Text hintText;
	public string[] loadingHints;


	void Start()
	{
		if (uiManager == null)
		{
			uiManager = UIManager.instance;
		}
	}

	void Update()
	{
		if (loadingScene)
		{
			loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
		}
	}

	public void LoadNewScene(int sceneIndex) {

		loadingText.text = "LOADING...";

		hintText.text = loadingHints [Random.Range (0, loadingHints.Length)];		

		newSceneIndex = sceneIndex;
		loadingScene = true;
		StartCoroutine (StartLoading());

		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	IEnumerator StartLoading()
	{
		yield return new WaitForSeconds (2.0f);

		AsyncOperation async = SceneManager.LoadSceneAsync (newSceneIndex);

		while (!async.isDone)
		{
			yield return null;
		}
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		print ("Loaded scene: " + scene.name);	
		SceneManager.sceneLoaded -= OnSceneLoaded;
		StartCoroutine (PostLoadDelay ());
	}

	IEnumerator PostLoadDelay()
	{
		yield return new WaitForSeconds (2.0f);

		uiManager.RemoveLoadingScreen ();
		loadingScene = false;
	}
}
