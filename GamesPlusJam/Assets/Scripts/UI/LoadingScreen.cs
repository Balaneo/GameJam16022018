using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour {

	[Header("Loading")]
	public Text loadingText;

	[Header("Hints")]
	public Text hintText;
	public string[] loadingHints;

	// Use this for initialization
	public void StartLoading () {

		loadingText.text = "LOADING...";

		hintText.text = loadingHints [Random.Range (0, loadingHints.Length)];		
	}
}
