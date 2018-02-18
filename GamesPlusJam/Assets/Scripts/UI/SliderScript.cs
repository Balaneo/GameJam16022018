using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour {

	public Text sliderValueText;

	public void UpdateSliderValueText(float newVolume)
	{
		if (sliderValueText != null)
		{
			sliderValueText.text = Mathf.Round(newVolume * 100).ToString() + "%";
		}
	}
}
