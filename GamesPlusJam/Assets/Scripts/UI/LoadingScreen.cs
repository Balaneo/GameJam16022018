using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour {

	private int scene;
	private bool loadingScene;

	[Header("Loading")]
	public Text loadingText;

	[Header("Hints")]
	public Text hintText;
	public string[] loadingHints;

	public delegate void LoadLevel();
	public event LoadLevel OnLoaded;


	void Update()
	{
		if (loadingScene)
		{
			loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
		}
	}

	public void LoadNewScene(int newScene) {

		loadingText.text = "LOADING...";

		hintText.text = loadingHints [Random.Range (0, loadingHints.Length)];		

		scene = newScene;
		loadingScene = true;
		StartCoroutine (StartLoading());
	}

	IEnumerator StartLoading()
	{
		yield return new WaitForSeconds (3.0f);

		AsyncOperation async = SceneManager.LoadSceneAsync (scene);

		while (!async.isDone)
		{
			yield return null;
		}

		if (OnLoaded != null)
		{
			OnLoaded ();
		}
	}
}
