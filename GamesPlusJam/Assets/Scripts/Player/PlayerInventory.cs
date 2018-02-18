using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour {

	public const int numInventorySlots = 5;

	public InventoryItem[] items = new InventoryItem[numInventorySlots];
	public string[] itemNames = new string[numInventorySlots];
	public string[] itemDescriptions = new string[numInventorySlots];
	public Image[] itemImages = new Image[numInventorySlots];
	public bool[] itemConsumedOnUses = new bool[numInventorySlots];

	public delegate void ItemAdded(InventoryItem newItem, int slotIndex);
	public static event ItemAdded OnItemAdded;

	public delegate void ItemRemoved(int slotIndex);
	public static event ItemRemoved OnItemRemoved;


	public bool CheckForItem(InventoryItem itemToLookFor)
	{
		bool itemFound = false;

		for (int i = 0; i < items.Length; i++)
		{
			if (itemToLookFor == items [i])
			{
				itemFound = true;
			}
		}

		return itemFound;
	}


	public void AddItem(InventoryItem newItem)
	{
		for (int i = 0; i < items.Length; i++)
		{
			if (items [i] == null)
			{
				print ("Player received the item: " + newItem.name + "! Placed in slot: " + i);

				items [i] = newItem;
				itemNames [i] = newItem.itemName;
				itemDescriptions [i] = newItem.itemDescription;
				//itemImages [i].sprite = newItem.itemIcon;
				itemConsumedOnUses [i] = newItem.itemConsumedOnUse;

				if (OnItemAdded != null)
				{
					OnItemAdded (newItem, i);
				}

				return;
			}
		}
	}

	public void RemoveItem(InventoryItem itemToRemove)
	{
		for (int i = 0; i < items.Length; i++)
		{
			if (items [i] == itemToRemove)
			{
				print ("Player used the item: " + itemNames[i] + "! Removed from slot: " + i);

				items [i] = null;
				itemNames [i] = null;
				itemDescriptions [i] = null;
				//itemImages [i].sprite = null;
				itemConsumedOnUses [i] = false;

				if (OnItemRemoved != null)
				{
					OnItemRemoved (i);
				}

				return;
			}
		}
	}
}
