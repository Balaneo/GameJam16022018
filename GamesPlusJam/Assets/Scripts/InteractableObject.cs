using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour {

	public bool destroyOnInteraction;

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
				if (requiredItem.itemConsumedOnUse)
				{
					player.GetComponent<PlayerInventory> ().RemoveItem (requiredItem);
				}
			} else
			{
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

		if (success && destroyOnInteraction)
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
