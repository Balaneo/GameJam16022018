using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour {

	public const int numInventorySlots = 5;

	public InventoryItem[] items = new InventoryItem[numInventorySlots];
	public Image[] itemImages = new Image[numInventorySlots];


	public void AddItem(InventoryItem newItem)
	{
		for (int i = 0; i < items.Length; i++)
		{
			if (items [i] == null)
			{
				items [i] = newItem;
				itemImages [i].sprite = newItem.itemIcon;
				itemImages [i].enabled = true;
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
				items [i] = null;
				itemImages [i].sprite = null;
				itemImages [i].enabled = false;
				return;
			}
		}
	}
}
