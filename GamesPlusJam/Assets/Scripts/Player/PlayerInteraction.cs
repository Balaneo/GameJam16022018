using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

	public GameObject currentInteractionTarget;

	void Update()
	{
		if (Input.GetButtonDown ("Action"))
		{
			if (currentInteractionTarget)
			{
				currentInteractionTarget.GetComponent<InteractableObject> ().Action (this.gameObject);
			} else
			{
				print ("I have no interaction target!");
			}
		}
	}
}
