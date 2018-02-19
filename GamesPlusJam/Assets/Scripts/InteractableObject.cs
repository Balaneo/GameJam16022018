using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour {

	public bool destroyOnSuccessfulInteraction;

	public InventoryItem requiredItem;
	public InventoryItem returnedItem;

	public delegate void ObjectInteraction(bool interactionSuccessful);
	public static event ObjectInteraction OnObjectInteraction;

	public bool Action(GameObject player)
	{
		bool success = true;

		if (requiredItem != null)
		{			
			if (player.GetComponent<PlayerInventory> ().CheckForItem (requiredItem))
			{
				player.GetComponent<PlayerInventory> ().UseItem (requiredItem);

			} else
			{
				print ("This item requires a : " + requiredItem.name);
				success = false;
			}
		}

		if (returnedItem != null && success == true)
		{
			player.GetComponent<PlayerInventory> ().AddItem (returnedItem);
		}

		if (OnObjectInteraction != null)
		{
			OnObjectInteraction (success);
		}

		if (success && destroyOnSuccessfulInteraction)
		{
			Destroy (this.gameObject);
		}

		return success;
	}

	void OnTriggerEnter(Collider other)
	{
		other.GetComponent<PlayerInteraction> ().currentInteractionTarget = this.gameObject;
	}

	void OnTriggerExit(Collider other)
	{
		if (other.GetComponent<PlayerInteraction> ().currentInteractionTarget == this.gameObject)
		{
			other.GetComponent<PlayerInteraction> ().currentInteractionTarget = null;
		}
	}
}
